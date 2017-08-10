using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using migh.api;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Threading;
using WMPLib;
using JsonTools;
using FileDownloader;

namespace migh.player
{
    public partial class frmPlayer : Form
    {
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;
        #region r
        protected override void WndProc(ref Message m)
        {
            const int wmNcHitTest = 0x84;
            const int htLeft = 10;
            const int htRight = 11;
            const int htTop = 12;
            const int htTopLeft = 13;
            const int htTopRight = 14;
            const int htBottom = 15;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;

            if (m.Msg == wmNcHitTest)
            {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
                ///allow resize on the lower left corner
                if (pt.X <= 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= 16 && pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - 16 && pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }
                ///allow resize on the top border
                if (pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htTop);
                    return;
                }
                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htBottom);
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htLeft);
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }

        public frmPlayer()
        {
            
            InitializeComponent();
        }

        private void frmPlayer_Load(object sender, EventArgs e)
        {
            AlbumCover.Image = migh.player.Properties.Resources.default_cover;
            Login();
        }
        class ListBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }
        int TogMove;
        int MValX;
        int MValY;
        
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("https://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        Library lib = new Library();
        User user = new User();
        
        int last_album_cover_id = -1;

        
        private void Login()
        {
            BackgroundWorker worker = new BackgroundWorker() { WorkerReportsProgress = true };
            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                worker.ReportProgress(0, "Conectando a internet...");
                while (!CheckForInternetConnection())
                {

                }
                try
                {
                    string su = "ftp://migh.somee.com/www.migh.somee.com/migh/migh.lib";
                    string u = "eduardohley";
                    string p = "@Poder123";
                    if(File.Exists(@"cookie.txt"))
                    {
                        JsonFile f = new JsonFile(Application.StartupPath, "cookie.txt");
                        Configuration.Cookie cookie = f.Read<Configuration.Cookie>();
                        su = Tools.DecodeStringFromBase64(cookie.su);
                        u = Tools.DecodeStringFromBase64(cookie.u);
                        p = Tools.DecodeStringFromBase64(cookie.p);
                    }
                    worker.ReportProgress(0, "Descargando datos...");
                    lib = new Library(su, u, p);
                    if(lib.user_list.Count > 0)
                    {
                        args.Result = "Solicitando datos de acceso...";
                    }
                    else
                    {
                        args.Result = "No se encontraron datos";
                    }
                }
                catch(Exception ex)
                {
                    args.Result = ex.Message;
                }
            };
            worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null)
                {
                    lblTitle.Invoke((MethodInvoker)(() => lblTitle.Text = args.Result.ToString()));
                    if(args.Result.ToString() == "Solicitando datos de acceso...")
                    {
                        PostLogin();
                    }
                }
            };
            worker.ProgressChanged += new ProgressChangedEventHandler(WorkerProgressChanged);
            worker.RunWorkerAsync();
        }
        private void PostLogin()
        {
            BackgroundWorker worker = new BackgroundWorker() { WorkerReportsProgress = true };
            if (lib.user_list.Count > 0)
            {
                frmLogin frm = new frmLogin();
                frm.lib = lib;
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                {
                    worker.DoWork += delegate(object s, DoWorkEventArgs args)
                    {
                        worker.ReportProgress(0, "Obteniendo datos de usuario...");
                        Thread.Sleep(500);
                        try
                        {
                            string username = frm.Username;
                            user = lib.user_list.Single(us => us.name.ToLower() == username.ToLower()); ;
                            worker.ReportProgress(0, "Cargando música...");
                            Thread.Sleep(500);
                            foreach (int i in user.artist_list)
                            {
                                Artist artist = Artist.get(lib.artist_list, i);
                                ListBoxItem item = new ListBoxItem();
                                item.Text = artist.name;
                                item.Value = artist;
                                listArtists.Invoke((MethodInvoker)(() => listArtists.Items.Add(item)));
                            }
                            args.Result = user.message;
                        }
                        catch (Exception ex)
                        {
                            args.Result = ex.Message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
                    {
                        if(args.Result != null)
                        {
                            lblTitle.Invoke((MethodInvoker)(() => lblTitle.Text = args.Result.ToString()));
                            try
                            {
                                listArtists.Invoke((MethodInvoker)(() => listArtists.SelectedIndex = 0));
                            }
                            catch { }
                        }
                    };
                    worker.ProgressChanged += new ProgressChangedEventHandler(WorkerProgressChanged);
                    worker.RunWorkerAsync();
                }
                
                lblTitle.AllowDrop = true;
                lblTitle.DragEnter += new DragEventHandler(lblTitle_DragEnter);
                lblTitle.DragDrop += new DragEventHandler(lblTitle_DragDrop);
            }
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string title = e.UserState.ToString();
            lblTitle.Text = title;
        }

        private void listArtists_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSongs.ContextMenuStrip = null;
            listAlbums.ContextMenuStrip = null;
            listAlbums.Items.Clear();
            listSongs.Items.Clear();
            try
            {
                Artist artist = (Artist)((ListBoxItem)listArtists.SelectedItem).Value;
                foreach(Album album in lib.album_list)
                {
                    if(album.artist_id == artist.id)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Text = album.name;
                        item.Value = album;
                        listAlbums.Items.Add(item);
                    }
                }
                foreach(Song song in lib.song_list)
                {
                    if(song.artist_id == artist.id)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Text = song.name;
                        item.Value = song;
                        listSongs.Items.Add(item);
                    }
                }
            }
            catch { }
        }

       
        #region TitleBarPanel
        private void TitleBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void TitleBarPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void TitleBarPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Estás seguro?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(Downloading)
                {
                    if (MessageBox.Show("Hay descargas en progreso. ¿Deseas cancelarlas y salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CancelCurrentDownload();
                        CancellAllDownloads();
                        Application.Exit();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void Minbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void Exitbtn_MouseEnter(object sender, EventArgs e)
        {
            Exitbtn.Image = Properties.Resources.close2_mouse;
        }

        private void Exitbtn_MouseLeave(object sender, EventArgs e)
        {
            Exitbtn.Image = Properties.Resources.close2;
        }

        private void Minbtn_MouseDown(object sender, MouseEventArgs e)
        {
            Minbtn.Image = Properties.Resources.min_mouse_pressed;
        }

        private void Exitbtn_MouseDown(object sender, MouseEventArgs e)
        {
            Exitbtn.Image = Properties.Resources.close2_mouse_pressed;
        }

        private void Minbtn_MouseEnter(object sender, EventArgs e)
        {
            Minbtn.Image = Properties.Resources.min_mouse;
        }

        private void Minbtn_MouseLeave(object sender, EventArgs e)
        {
            Minbtn.Image = Properties.Resources.min;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog(this);
        }

        private void btnSettings_MouseDown(object sender, MouseEventArgs e)
        {
            btnSettings.Image = Properties.Resources.Settings_mouse_pressed;
        }

        private void btnSettings_MouseLeave(object sender, EventArgs e)
        {
            btnSettings.Image = Properties.Resources.Settings;
        }

        private void btnSettings_MouseEnter(object sender, EventArgs e)
        {
            btnSettings.Image = Properties.Resources.Settings_mouse;
        }
        #endregion
        
        List<Song> song_playlist = new List<Song>();

        Song FindSongByURL(string url)
        {
            foreach(Song song in lib.song_list)
            {
                Artist artist = Artist.get(lib.artist_list, song.artist_id);
                Album album = Album.get(lib.album_list, song.album_id);

                string song_url = string.Format(lib.configuration.AudioFileURLFormat, artist.url_name, album.url_name, Tools.ConvertToGitHubFile(song.file_name, lib.configuration.GitHubFile_TextToReplace_List));

                if(url.ToLower() == song_url.ToLower())
                {
                    return song;
                }
            }
            return null;
        }
        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    //if(MusicPlayer.settings.getMode("loop"))
        //    //{
        //    //    btnRepeat.Image = Properties.Resources.Repeat_on;
        //    //}
        //    //else
        //    //{
        //    //    btnRepeat.Image = Properties.Resources.Repeat_off;
        //    //}
        //    //if (MusicPlayer.settings.getMode("shuffle"))
        //    //{
        //    //    btnShuffle.Image = Properties.Resources.Shuffle_on;
        //    //}
        //    //else
        //    //{
        //    //    btnShuffle.Image = Properties.Resources.Shuffle_off;
        //    //}

        //    if (MusicPlayer.playState == WMPPlayState.wmppsPlaying)
        //    {
        //        try
        //        {
        //            if (MusicPlayer.Ctlcontrols.currentItem != null)
        //            {

        //                //string song_file_name = MusicPlayer.Ctlcontrols.currentItem.name + ".m4a";
        //                //string song_url_name = Tools.ConvertToGitHubFile(song_file_name, lib.configuration.GitHubFile_TextToReplace_List);
        //                //int index = 0;
        //                //for (int i = 0; i < MusicPlayer.currentPlaylist.count - 1; i++)
        //                //{
        //                //    if (MusicPlayer.currentMedia.isIdentical[playlist.Item[i]])
        //                //    {
        //                //        index = i;
        //                //        break;
        //                //    }
        //                //}
        //                if (MusicPlayer.currentPlaylist.count == song_playlist.Count)
        //                {
        //                    //Song song = song_playlist.ElementAt(index);
        //                    //Album album = Album.get(lib.album_list, song.album_id);
        //                    //Artist artist = Artist.get(lib.artist_list, song.artist_id);
        //                    //lblSong_Title.Text = song.name;
        //                    //lblSong_Artist.Text = artist.name;
        //                    //lblSong_album.Text = album.name;
        //                    if (MusicPlayer.Ctlcontrols.currentPositionString == "" || MusicPlayer.Ctlcontrols.currentPositionString == null)
        //                    {
        //                        lblSong_duration.Text = MusicPlayer.currentMedia.durationString;
        //                    }
        //                    else
        //                    {
        //                        lblSong_duration.Text = MusicPlayer.Ctlcontrols.currentPositionString + " / " + MusicPlayer.currentMedia.durationString;
        //                    }
        //                }
        //                //string audio_url = string.Format(Configuration.Data.AudioFileURLFormat, 
        //                // lblTitle.Text = song_file;
        //            }
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}

        private void AlbumCover_DoubleClick(object sender, EventArgs e)
        {
            //string FolderCheckFormat = "https://github.com/505darksoft/M/tree/master/{0}/{1}";
            //frmSongStatus frm = new frmSongStatus();
            //frm.FolderCheckFormat = FolderCheckFormat;
            //frm.AudioFileURLFormat = Configuration.Data.AudioFileURLFormat;
            //frm.user = user;
            //frm.lib = lib;

            //frm.Show();
        }

        private void MusicPlayer_CurrentItemChange(object sender, AxWMPLib._WMPOCXEvents_CurrentItemChangeEvent e)
        {
            //string url = MusicPlayer.Ctlcontrols.currentItem.sourceURL;
            //Song selectedSong = FindSongByURL(url);
            //if (selectedSong != null)
            //{
            //    try
            //    {
            //        Artist artist = Artist.get(lib.artist_list, selectedSong.artist_id);
            //        Album album = Album.get(lib.album_list, selectedSong.album_id);
            //        lblSong_Title.Text = selectedSong.name;
            //        lblSong_Title.Tag = selectedSong;

            //        lblSong_Artist.Text = artist.name;
            //        lblSong_Artist.Tag = artist;

            //        lblSong_album.Text = album.name;
            //        lblSong_album.Tag = album;
            //    }
            //    catch { }
            //}
            
            //int index = 0;
            //for (int i = 0; i < MusicPlayer.currentPlaylist.count - 1; i++)
            //{
            //    if (MusicPlayer.currentMedia.isIdentical[playlist.Item[i]])
            //    {
            //        index = i;
            //        break;
            //    }
            //}
            //Image img = null;
            //Song song = new Song();
            //if (MusicPlayer.currentPlaylist.count == song_playlist.Count)
            //{
            //    song = song_playlist.ElementAt(index);
            //}
            //try
            //{
            //    if (song.album_id != last_album_cover_id)
            //    {
            //        AlbumCover.Image = Properties.Resources.default_cover;
            //        BackgroundWorker ImageChangeWorker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            //        ImageChangeWorker.DoWork += delegate(object s, DoWorkEventArgs args)
            //        {
            //            try
            //            {
            //                Album album = Album.get(lib.album_list, song.album_id);
            //                Artist artist = Artist.get(lib.artist_list, song.artist_id);

            //                string album_cover_url = string.Format(lib.configuration.AlbumCoverImageFileURLFormat, artist.url_name, album.url_name);
            //                img = Tools.DownloadImage(album_cover_url);
            //                img = Tools.ResizeImage(img, AlbumCover.Width, AlbumCover.Height);
            //            }
            //            catch { }

            //            if (img != null)
            //            {
            //                AlbumCover.Invoke((MethodInvoker)(() => AlbumCover.Image = img));
            //            }
            //            last_album_cover_id = song.album_id;
            //        };
            //        ImageChangeWorker.RunWorkerAsync();
            //    }
            //}
            //catch
            //{

            //}
        }

        //private void Timer_Reset_Tick(object sender, EventArgs e)
        //{
        //    Timer.Stop();
        //    Timer.Dispose();
        //    //MusicPlayer.settings.per
        //    if (MusicPlayer.playState != WMPPlayState.wmppsStopped || MusicPlayer.playState != WMPPlayState.wmppsPaused)
        //    {
        //        Timer.Start();
        //    }
        //}

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            if(MusicPlayer.settings.getMode("loop"))
            {
                MusicPlayer.settings.setMode("loop", false);
                btnRepeat.Image = Properties.Resources.Repeat_off;
            }
            else
            {
                MusicPlayer.settings.setMode("loop", true);
                btnRepeat.Image = Properties.Resources.Repeat_on;
            }
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            if (MusicPlayer.settings.getMode("shuffle"))
            {
                MusicPlayer.settings.setMode("shuffle", false);
                btnShuffle.Image = Properties.Resources.Shuffle_off;
            }
            else
            {
                MusicPlayer.settings.setMode("shuffle", true);
                btnShuffle.Image = Properties.Resources.Shuffle_on;
            }
        }

        private void MusicPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //if (MusicPlayer.playState == WMPPlayState.wmppsPaused || MusicPlayer.playState == WMPPlayState.wmppsStopped)
            //{
            //    Timer.Stop();
            //    Timer.Dispose();
            //}
            //else
            //{
            //    Timer.Stop();
            //    Timer.Start();
            //}
            if (MusicPlayer.playState == WMPPlayState.wmppsTransitioning)
            {
                //Timer.Stop();
                //Timer.Enabled = true;
                //Timer.Start();
                //Timer_Reset.Stop();
                //Timer_Reset.Enabled = true;
                //Timer_Reset.Start();
                //if(MusicPlayer.Ctlcontrols.currentItem != null)
                //{
                //    string url = MusicPlayer.Ctlcontrols.currentItem.sourceURL;
                //    Song song = FindSongByURL(url);
                //    Artist artist = Artist.get(lib.artist_list, song.artist_id);
                //    Album album = Album.get(lib.album_list, song.album_id);

                //    lblSong_Title.Text = song.name;
                //    lblSong_Title.Tag = song;

                //    lblSong_Artist.Text = artist.name;
                //    lblSong_Artist.Tag = artist;

                //    lblSong_album.Text = album.name;
                //    lblSong_album.Tag = album;
                //}
                lblLoading.Visible = true;
            }
            else
            {
                lblLoading.Visible = false;
            }
            if (MusicPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                string url = MusicPlayer.Ctlcontrols.currentItem.sourceURL;
                Song song = FindSongByURL(url);
                //if (selectedSong != null)
                //{
                //    try
                //    {
                //        Artist artist = Artist.get(lib.artist_list, selectedSong.artist_id);
                //        Album album = Album.get(lib.album_list, selectedSong.album_id);
                //        lblSong_Title.Text = selectedSong.name;
                //        lblSong_Title.Tag = selectedSong;

                //        lblSong_Artist.Text = artist.name;
                //        lblSong_Artist.Tag = artist;

                //        lblSong_album.Text = album.name;
                //        lblSong_album.Tag = album;
                //    }
                //    catch { }
                //}

                //int index = 0;
                //for (int i = 0; i < MusicPlayer.currentPlaylist.count - 1; i++)
                //{
                //    if (MusicPlayer.currentMedia.isIdentical[playlist.Item[i]])
                //    {
                //        index = i;
                //        break;
                //    }
                //}
                Image img = null;
                //Song song = new Song();
                //if (MusicPlayer.currentPlaylist.count == song_playlist.Count)
                //{
                //    song = song_playlist.ElementAt(index);
                //}
                try
                {
                    Artist _artist = Artist.get(lib.artist_list, song.artist_id);
                    Album _album = Album.get(lib.album_list, song.album_id);
                    lblSong_Title.Text = song.name;
                    lblSong_Title.Tag = song;

                    lblSong_Artist.Text = _artist.name;
                    lblSong_Artist.Tag = _artist;

                    lblSong_album.Text = _album.name;
                    lblSong_album.Tag = _album;

                    if (song.album_id != last_album_cover_id)
                    {
                        AlbumCover.Image = Properties.Resources.default_cover;
                        BackgroundWorker ImageChangeWorker = new BackgroundWorker() { WorkerSupportsCancellation = true };
                        ImageChangeWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                        {
                            try
                            {
                                Album album = Album.get(lib.album_list, song.album_id);
                                Artist artist = Artist.get(lib.artist_list, song.artist_id);

                                string album_cover_url = string.Format(lib.configuration.AlbumCoverImageFileURLFormat, artist.url_name, album.url_name);
                                img = Tools.DownloadImage(album_cover_url);
                                img = Tools.ResizeImage(img, AlbumCover.Width, AlbumCover.Height);
                            }
                            catch { }

                            if (img != null)
                            {
                                AlbumCover.Invoke((MethodInvoker)(() => AlbumCover.Image = img));
                            }
                            last_album_cover_id = song.album_id;
                        };
                        ImageChangeWorker.RunWorkerAsync();
                    }
                }
                catch
                {

                }
                //Timer.Start();
            }
            else
            {

            }
        }

        Artist FindArtistByName(string name)
        {
            try
            {
                foreach (Artist a in lib.artist_list)
                {
                    if (a.name.ToLower() == name.ToLower())
                    {
                        return a;
                    }
                }
                return null;
            }
            catch 
            {
                return null;
            }
        }
        

        #region Selección de lista
        private void SelectArtist(Artist artist)
        {
            if (artist != null)
            {
                int index = 0;
                foreach (ListBoxItem item in listArtists.Items)
                {
                    Artist art = (Artist)item.Value;
                    if (artist.name.ToLower() == art.name.ToLower())
                    {
                        listArtists.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
        }

        private void SelectAlbum(Artist artist, Album album)
        {
            if (artist != null && album != null)
            {
                foreach (Album a in lib.album_list)
                {
                    if (a.artist_id == artist.id && a.name.ToLower() == album.name.ToLower())
                    {
                        int index = 0;
                        foreach (ListBoxItem item in listArtists.Items)
                        {
                            Artist art = (Artist)item.Value;
                            if (artist.name.ToLower() == art.name.ToLower())
                            {
                                listArtists.SelectedIndex = index;
                                break;
                            }
                            index++;
                        }
                        int i = 0;
                        foreach (ListBoxItem item in listAlbums.Items)
                        {
                            Album alb = (Album)item.Value;
                            if (album.name.ToLower() == alb.name.ToLower())
                            {
                                listAlbums.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void SelectSong(Artist artist, Album album, Song song)
        {
            if (artist != null && album != null && song != null)
            {
                foreach (Song s in lib.song_list)
                {
                    if (s.artist_id == artist.id && s.album_id == album.id && s.name.ToLower() == song.name.ToLower())
                    {
                        int artistIndex = 0;
                        foreach (ListBoxItem item in listArtists.Items)
                        {
                            Artist art = (Artist)item.Value;
                            if (artist.name.ToLower() == art.name.ToLower())
                            {
                                listArtists.SelectedIndex = artistIndex;
                                break;
                            }
                            artistIndex++;
                        }
                        int albumIndex = 0;
                        foreach (ListBoxItem item in listAlbums.Items)
                        {
                            Album alb = (Album)item.Value;
                            if (album.name.ToLower() == alb.name.ToLower())
                            {
                                listAlbums.SelectedIndex = albumIndex;
                                break;
                            }
                            albumIndex++;
                        }
                        int songIndex = 0;
                        foreach (ListBoxItem item in listSongs.Items)
                        {
                            Song son = (Song)item.Value;
                            if (son.name.ToLower() == song.name.ToLower())
                            {
                                listSongs.SelectedIndex = songIndex;
                                break;
                            }
                            songIndex++;
                        }
                    }
                }
            }
        }
        #endregion

        private void lblSong_Artist_DoubleClick(object sender, EventArgs e)
        {
            
        }
        private void lblSong_album_DoubleClick(object sender, EventArgs e)
        {
            
        }

        #region search
        private void Search()
        {
            cbxSearch.DataSource = null;
            //cbxSearch.DroppedDown = false;
            string text = txtSearch.Text;
            if (text != "")
            {
                List<Artist> artists = SearchArtistsByName(text);
                List<Album> albums = SearchAlbumsByName(text);
                List<Song> songs = SearchSongsByName(text);

                List<SearchItem> search_result = new List<SearchItem>();
                if (artists.Count > 0)
                {
                    SearchItem first = new SearchItem();
                    first.Text = "ARTISTAS";
                    first.Type = SearchItemType.None;
                    first.Value = null;
                    search_result.Add(first);
                    foreach (Artist artist in artists)
                    {
                        SearchItem item = new SearchItem();
                        item.Text = artist.name;
                        item.Artist = artist.name;
                        item.Type = SearchItemType.Artist;
                        item.Value = artist;
                        search_result.Add(item);
                    }
                    SearchItem last = new SearchItem();
                    last.Text = "";
                    last.Type = SearchItemType.None;
                    last.Value = null;
                    search_result.Add(last);
                }
                if (albums.Count > 0)
                {
                    SearchItem first = new SearchItem();
                    first.Text = "ÁLBUMES";
                    first.Type = SearchItemType.None;
                    first.Value = null;
                    search_result.Add(first);
                    foreach (Album album in albums)
                    {
                        Artist art = Artist.get(lib.artist_list, album.artist_id);
                        SearchItem item = new SearchItem();
                        item.Text = album.name;
                        item.Artist = art.name;
                        item.Type = SearchItemType.Album;
                        item.Value = album;
                        search_result.Add(item);
                    }
                    SearchItem last = new SearchItem();
                    last.Text = "";
                    last.Type = SearchItemType.None;
                    last.Value = null;
                    search_result.Add(last);
                }
                if (songs.Count > 0)
                {
                    SearchItem first = new SearchItem();
                    first.Text = "CANCIONES";
                    first.Type = SearchItemType.None;
                    first.Value = null;
                    search_result.Add(first);
                    foreach (Song song in songs)
                    {
                        Artist art = Artist.get(lib.artist_list, song.artist_id);
                        SearchItem item = new SearchItem();
                        item.Text = song.name;
                        item.Artist = art.name;
                        item.Type = SearchItemType.Song;
                        item.Value = song;
                        search_result.Add(item);
                    }
                }
                if(search_result.Count > 0)
                {
                    cbxSearch.DataSource = search_result;
                    cbxSearch.DroppedDown = true;
                    Cursor.Current = Cursors.Default; 
                }
            }
        }

        private static class SearchItemType
        {
            public const string Artist = "Artist";
            public const string Album = "Album";
            public const string Song = "Song";
            public const string None = "None";
        }
        private class SearchItem
        {
            public string Text { get; set; }
            public string Type { get; set; }
            public string Artist { get; set; }
            public object Value { get; set; }
            public override string ToString()
            {
                if(this.Type == SearchItemType.Artist)
                {
                    return " •  " + Text;
                }
                if (this.Type == SearchItemType.Song || this.Type == SearchItemType.Album)
                {
                    return " •  " + Text + " - " + Artist;
                }
                return Text;
            }
        }
        private List<Artist> SearchArtistsByName(string name)
        {
            List<Artist> list = new List<Artist>();
            foreach(int i in user.artist_list)
            {
                Artist artist = Artist.get(lib.artist_list, i);
                if(artist.name.ToLower().Contains(name.ToLower()))
                {
                    list.Add(artist);
                }
            }
            return list;
        }
        private List<Album> SearchAlbumsByName(string name)
        {
            List<Album> list = new List<Album>();
            foreach (int i in user.artist_list)
            {
                Artist artist = Artist.get(lib.artist_list, i);
                foreach(Album album in lib.album_list)
                {
                    if(album.artist_id == artist.id)
                    {
                        if(album.name.ToLower().Contains(name.ToLower()))
                        {
                            list.Add(album);
                        }
                    }
                }
            }
            return list;
        }
        private List<Song> SearchSongsByName(string name)
        {
            List<Song> list = new List<Song>();
            foreach (int i in user.artist_list)
            {
                Artist artist = Artist.get(lib.artist_list, i);
                foreach (Album album in lib.album_list)
                {
                    if (album.artist_id == artist.id)
                    {
                        foreach (Song song in lib.song_list)
                        {
                            if(song.album_id == album.id && song.artist_id == artist.id)
                            {
                                if(song.name.ToLower().Contains(name.ToLower()))
                                {
                                    list.Add(song);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
        #endregion
        #region cbxSearch
        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxSearch.SelectedItem != null)
            {
                try
                {
                    SearchItem item = (SearchItem)cbxSearch.SelectedItem;
                    if(item.Type == SearchItemType.None)
                    {
                        cbxSearch.DroppedDown = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    //txtSearch.Text = item.Text;
                    if(item.Type == SearchItemType.Artist)
                    {
                        SelectArtist((Artist)item.Value);
                    }
                    if(item.Type == SearchItemType.Album)
                    {
                        Album album = (Album)item.Value;
                        Artist artist = Artist.get(lib.artist_list, album.artist_id);
                        SelectAlbum(artist, album);
                    }
                    if(item.Type == SearchItemType.Song)
                    {
                        Song song = (Song)item.Value;
                        Album album = Album.get(lib.album_list, song.album_id);
                        Artist artist = Artist.get(lib.artist_list, song.artist_id);
                        SelectSong(artist, album, song);
                    }
                }
                catch { }
            }
        }
        #endregion
        #region txtSearch
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(!txtSearch.Text.StartsWith(":"))
            {
                Search();
            }
            if(txtSearch.Text == "")
            {
                cbxSearch.DroppedDown = false;
                cbxSearch.Items.Clear();
                cbxSearch.SelectedIndex = -1;
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text == "::download.library")
                {
                    foreach (Song song in lib.song_list)
                    {
                        Artist artist = Artist.get(lib.artist_list, song.artist_id);
                        DownloadItem item = new DownloadItem();
                        item.song = song;
                        item.artist = artist.name;
                        bool exists = false;
                        foreach (DownloadItem i in DownloadList)
                        {
                            if (i.song == song)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            DownloadList.Add(item);
                        }
                    }

                    if (DownloadIndex == 0 && !Downloading)
                    {
                        TriggerDownload();
                    }
                    txtSearch.Text = "";
                    return;
                }
                if (txtSearch.Text == ":song.count")
                {
                    txtSearch.Text = lib.song_list.Count.ToString();
                    return;
                }
                if (txtSearch.Text == ":restart")
                {
                    Application.Restart();
                }
                if (txtSearch.Text == ":song.status")
                {
                    string FolderCheckFormat = lib.configuration.AlbumCoverImageFileURLFormat.Replace("blob", "tree").Replace("/Cover.jpg?raw=true", "");
                    frmSongStatus frm = new frmSongStatus();
                    frm.FolderCheckFormat = FolderCheckFormat;
                    frm.AudioFileURLFormat = Configuration.Data.AudioFileURLFormat;
                    frm.user = user;
                    frm.lib = lib;
                    txtSearch.Text = "";
                    frm.Show();
                    return;
                }
            }
        }
        #endregion

        #region descarga
        public static void CancelCurrentDownload()
        {
            if (DownloadList.Count > 0)
            {
                fileDownloader.CancelDownloadAsync();
            }
        }
        public static void CancellAllDownloads()
        {
            if (DownloadList.Count > 0)
            {
                fileDownloader.CancelDownloadAsync();
                DownloadList.Clear();
                Downloading = false;
            }
        }

        void DownloadItemClick(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if(clickedItem.Tag is Song)
            {
                Song song = (Song)clickedItem.Tag;
                Artist artist = Artist.get(lib.artist_list, song.artist_id);
                DownloadItem item = new DownloadItem();
                item.song = song;
                item.artist = artist.name;
                DownloadList.Add(item);
            }
            if(clickedItem.Tag is Album)
            {
                Album album = (Album)clickedItem.Tag;
                Artist artist = Artist.get(lib.artist_list, album.artist_id);
                List<Song> songs = Song.getByAlbum(lib.song_list, album.id);

                foreach (Song song in songs)
                {
                    DownloadItem item = new DownloadItem();
                    item.song = song;
                    item.artist = artist.name;
                    bool exists = false;
                    foreach(DownloadItem i in DownloadList)
                    {
                        if(i.song == song)
                        {
                            exists = true;
                        }
                    }
                    if(!exists)
                    {
                        DownloadList.Add(item);
                    }
                }
            }
            if(DownloadIndex == 0 && !Downloading)
            {
                TriggerDownload();
            }
        }
        void TriggerDownload()
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                if (DownloadList.Count > DownloadIndex)
                {
                    DownloadItem item = DownloadList.ElementAt(DownloadIndex);
                    
                    Song song = item.song;
                    Artist artist = Artist.get(lib.artist_list, song.artist_id);
                    Album album = Album.get(lib.album_list, song.album_id);
                    Downloading = true;
                    if(File.Exists(Application.StartupPath + "\\Descargas\\" + artist.url_name + "\\" + album.url_name + "\\" + song.file_name))
                    {
                        DownloadList.ElementAt(DownloadIndex).status = "Completado";
                        DownloadList.ElementAt(DownloadIndex).progress = 100;
                        DownloadIndex++;
                        TriggerDownload();
                    }
                    else
                    {
                        DownloadList.ElementAt(DownloadIndex).status = "En progreso";
                        currentDownload = item.song.name + " - " + item.artist;
                        DownloadSong(song);
                    }
                }
                else
                {
                    DownloadList.Clear();
                    DownloadIndex = 0;
                    currentDownload = "";
                    Downloading = false;
                    MessageBox.Show("Se han completado las descargas", "Descargar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            worker.RunWorkerAsync();
        }
        public class DownloadItem
        {
            public Song song { get; set; }
            public string artist { get; set; }
            public string status = "Pendiente";
            public int progress = 0;
            public override string ToString()
            {
                
                return "[" + status + "]" + "(" + progress + "%)     " + song.name + " - " + artist ;
            }
        }
        public static bool Downloading = false;
        public static string currentDownload = "";
        int DownloadIndex = 0;
        public static List<DownloadItem> DownloadList = new List<DownloadItem>();
        public static IFileDownloader fileDownloader = new FileDownloader.FileDownloader();

        void DownloadSong(Song song)
        {
            Artist artist = Artist.get(lib.artist_list, song.artist_id);
            Album album = Album.get(lib.album_list, song.album_id);
            string file_url = string.Format(lib.configuration.AudioFileURLFormat, artist.url_name, album.url_name, Tools.ConvertToGitHubFile(song.file_name, lib.configuration.GitHubFile_TextToReplace_List));
            string downloadDestinationPath = Application.StartupPath + "\\Descargas\\" + artist.url_name + "\\" + album.url_name;
            string fileName = downloadDestinationPath + "\\" + song.file_name;
            if (!Directory.Exists(downloadDestinationPath))
            {
                Directory.CreateDirectory(downloadDestinationPath);
            }

            Uri uri = new Uri(file_url);
            fileDownloader = new FileDownloader.FileDownloader();
            fileDownloader.DownloadFileCompleted += DownloadFileCompleted;
            fileDownloader.DownloadProgressChanged += OnDownloadProgressChanged;
            fileDownloader.DownloadFileAsync(uri, fileName);
        }
        void OnDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            long total = args.TotalBytesToReceive;
            long current = args.BytesReceived;
            int p = Convert.ToInt32((current*100)/total);
            if (DownloadList.Count > DownloadIndex)
            {
                DownloadList.ElementAt(DownloadIndex).progress = p;
                DownloadList.ElementAt(DownloadIndex).status = "En progreso";
            }
            lblTitle.Invoke((MethodInvoker)(() => lblTitle.Text = string.Format("Descargando: {0} ({1}%)", currentDownload, p)));
        }
        void DownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            if(eventArgs.State == CompletedState.Succeeded)
            {
                try
                {
                    if (DownloadList.Count > 0 && DownloadList.Count > DownloadIndex)
                    DownloadList.ElementAt(DownloadIndex).status = "Completado";
                }
                catch
                {
                    
                }
            }
            if (eventArgs.State == CompletedState.Failed)
            {
                try
                {
                    if (DownloadList.Count > 0 && DownloadList.Count > DownloadIndex)
                        DownloadList.ElementAt(DownloadIndex).status = "Fallido";
                }
                catch
                {
                    
                }
            }
            if(eventArgs.State == CompletedState.Canceled)
            {
                try
                {
                    if (DownloadList.Count > 0 && DownloadList.Count > DownloadIndex)
                        DownloadList.ElementAt(DownloadIndex).status = "Cancelado";
                        DownloadList.ElementAt(DownloadIndex).progress = 0;
                }
                catch
                {
                    
                }
            }
            DownloadIndex++;
            lblTitle.Invoke((MethodInvoker)(() => lblTitle.Text = user.message));
            TriggerDownload();
        }
        #endregion

        #region listSongs
        private void listSongs_DoubleClick(object sender, EventArgs e)
        {
            AxWMPLib.AxWindowsMediaPlayer aux = MusicPlayer;
            if (listSongs.SelectedItems.Count == 1)
            {
                song_playlist.Clear();
                //Song song = (Song)((ListBoxItem)listSongs.SelectedItem).Value;
                try
                {
                    int index = 0;
                    WMPLib.IWMPMedia media = null;
                    WMPLib.IWMPPlaylist playlist = MusicPlayer.playlistCollection.newPlaylist("lista");

                    ListBoxItem si = (ListBoxItem)listSongs.SelectedItem;
                    index = listSongs.Items.IndexOf(si);

                    int i = 0;
                    foreach (ListBoxItem item in listSongs.Items)
                    {
                        //if (i >= index)
                        //{
                        Song s = (Song)item.Value;
                        Album album = Album.get(lib.album_list, s.album_id);
                        Artist artist = Artist.get(lib.artist_list, s.artist_id);
                        string artist_name = artist.url_name;
                        string album_name = album.url_name;
                        string song_name = Tools.ConvertToGitHubFile(s.url_name, lib.configuration.GitHubFile_TextToReplace_List);
                        string ip = string.Format(lib.configuration.AudioFileURLFormat, artist_name, album_name, song_name);
                        media = MusicPlayer.newMedia(ip);
                        song_playlist.Add(s);
                        playlist.appendItem(media);
                        //}
                        //i++;
                    }

                    if (MusicPlayer.settings.getMode("shuffle"))
                    {
                        MusicPlayer.settings.setMode("shuffle", false);
                        MusicPlayer.currentPlaylist = playlist;
                        MusicPlayer.settings.setMode("shuffle", true);
                    }
                    else
                    {
                        MusicPlayer.currentPlaylist = playlist;
                    }
                    if (listSongs.SelectedItems.Count == 1)
                    {
                        media = MusicPlayer.currentPlaylist.get_Item(index);
                    }
                    //MusicPlayer.Ctlcontrols.playItem(media);

                    string url = media.sourceURL;
                    Song song = FindSongByURL(url);

                    Image img = null;

                    try
                    {
                        Artist _artist = Artist.get(lib.artist_list, song.artist_id);
                        Album _album = Album.get(lib.album_list, song.album_id);
                        lblSong_Title.Text = song.name;
                        lblSong_Title.Tag = song;

                        lblSong_Artist.Text = _artist.name;
                        lblSong_Artist.Tag = _artist;

                        lblSong_album.Text = _album.name;
                        lblSong_album.Tag = _album;

                        if (song.album_id != last_album_cover_id)
                        {
                            AlbumCover.Image = Properties.Resources.default_cover;
                            BackgroundWorker ImageChangeWorker = new BackgroundWorker() { WorkerSupportsCancellation = true };
                            ImageChangeWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                            {
                                try
                                {
                                    Album album = Album.get(lib.album_list, song.album_id);
                                    Artist artist = Artist.get(lib.artist_list, song.artist_id);

                                    string album_cover_url = string.Format(lib.configuration.AlbumCoverImageFileURLFormat, artist.url_name, album.url_name);
                                    img = Tools.DownloadImage(album_cover_url);
                                    img = Tools.ResizeImage(img, AlbumCover.Width, AlbumCover.Height);
                                }
                                catch { }

                                if (img != null)
                                {
                                    AlbumCover.Invoke((MethodInvoker)(() => AlbumCover.Image = img));
                                }
                                last_album_cover_id = song.album_id;
                            };
                            ImageChangeWorker.RunWorkerAsync();
                        }
                    }
                    catch
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region listAlbums
        private void listAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSongs.ContextMenuStrip = null;
            listSongs.Items.Clear();
            try
            {
                Album album = (Album)((ListBoxItem)listAlbums.SelectedItem).Value;

                foreach (Song song in lib.song_list)
                {
                    if (song.album_id == album.id)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Text = song.name;
                        item.Value = song;
                        listSongs.Items.Add(item);
                    }
                }

            }
            catch { }
        }

        private void listAlbums_DoubleClick(object sender, EventArgs e)
        {
            //playlist = MusicPlayer.playlistCollection.newPlaylist("lista");
            //foreach (ListBoxItem item in listSongs.Items)
            //{
            //    media = MusicPlayer.newMedia(((Song)item.Value).ip_url);
            //    playlist.appendItem(media);
            //}
            //MusicPlayer.currentPlaylist = playlist;
            //MusicPlayer.Ctlcontrols.play();
        }
        #endregion

        
        private void btnDownload_Click(object sender, EventArgs e)
        {
            
        }

        object DragItem = null;

        private void lblTitle_DragDrop(object sender, DragEventArgs e)
        {
            if (DragItem != null)
            {
                if (DragItem is Song)
                {
                    Song song = (Song)DragItem;
                    Artist artist = Artist.get(lib.artist_list, song.artist_id);
                    DownloadItem item = new DownloadItem();
                    item.song = song;
                    item.artist = artist.name;
                    bool exists = false;
                    foreach (DownloadItem i in DownloadList)
                    {
                        if (i.song == song)
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        DownloadList.Add(item);
                    }
                }
                if (DragItem is Album)
                {
                    Album album = (Album)DragItem;
                    Artist artist = Artist.get(lib.artist_list, album.artist_id);
                    List<Song> songs = Song.getByAlbum(lib.song_list, album.id);

                    foreach (Song song in songs)
                    {
                        DownloadItem item = new DownloadItem();
                        item.song = song;
                        item.artist = artist.name;
                        bool exists = false;
                        foreach (DownloadItem i in DownloadList)
                        {
                            if (i.song == song)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            DownloadList.Add(item);
                        }
                    }
                }
                if (DragItem is Artist)
                {
                    Artist artist = (Artist)DragItem;
                    List<Song> songs = Song.getByArtist(lib.song_list, artist.id);

                    foreach (Song song in songs)
                    {
                        DownloadItem item = new DownloadItem();
                        item.song = song;
                        item.artist = artist.name;
                        bool exists = false;
                        foreach (DownloadItem i in DownloadList)
                        {
                            if (i.song == song)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            DownloadList.Add(item);
                        }
                    }
                }
                if (DownloadIndex == 0 && !Downloading)
                {
                    TriggerDownload();
                }
                DragItem = null;
            }
        }

        private void lblSong_Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (user.premium && ModifierKeys.HasFlag(Keys.Control))
            {
                if(lblSong_Title.Tag != null)
                {
                    if(lblSong_Title.Tag is Song)
                    {
                        DragItem = lblSong_Title.Tag as Song;
                        DragDropEffects dde1 = DoDragDrop(DragItem, DragDropEffects.Copy);
                    }
                }
            }
        }

        private void lblSong_Artist_MouseDown(object sender, MouseEventArgs e)
        {
            if (user.premium && ModifierKeys.HasFlag(Keys.Control))
            {
                if (lblSong_Artist.Tag != null)
                {
                    if (lblSong_Artist.Tag is Artist)
                    {
                        DragItem = lblSong_Artist.Tag as Artist;
                        DragDropEffects dde1 = DoDragDrop(DragItem, DragDropEffects.Copy);
                    }
                }
            }
        }

        private void lblSong_album_MouseDown(object sender, MouseEventArgs e)
        {
            if (user.premium && ModifierKeys.HasFlag(Keys.Control))
            {
                if (lblSong_album.Tag != null)
                {
                    if (lblSong_album.Tag is Album)
                    {
                        DragItem = lblSong_album.Tag as Album;
                        DragDropEffects dde1 = DoDragDrop(DragItem, DragDropEffects.Copy);
                    }
                }
            }
        }

        private void lblTitle_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            frmDownload frm = new frmDownload();
            frm.ShowDialog(this);
        }

        private void listSongs_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(listSongs.SelectedItems.Count == 1)
                {
                    song_playlist.Clear();

                    //Song song = (Song)((ListBoxItem)listSongs.SelectedItem).Value;
                    try
                    {
                        int index = 0;
                        WMPLib.IWMPMedia media = null;
                        WMPLib.IWMPPlaylist playlist = MusicPlayer.playlistCollection.newPlaylist("lista");

                        ListBoxItem si = (ListBoxItem)listSongs.SelectedItem;
                        index = listSongs.Items.IndexOf(si);

                        int i = 0;
                        foreach (ListBoxItem item in listSongs.Items)
                        {
                            //if (i >= index)
                            //{
                            Song s = (Song)item.Value;
                            Album album = Album.get(lib.album_list, s.album_id);
                            Artist artist = Artist.get(lib.artist_list, s.artist_id);
                            string artist_name = artist.url_name;
                            string album_name = album.url_name;
                            string song_name = Tools.ConvertToGitHubFile(s.url_name, lib.configuration.GitHubFile_TextToReplace_List);
                            string ip = string.Format(lib.configuration.AudioFileURLFormat, artist_name, album_name, song_name);
                            media = MusicPlayer.newMedia(ip);
                            song_playlist.Add(s);
                            playlist.appendItem(media);
                            //}
                            //i++;
                        }

                        if (MusicPlayer.settings.getMode("shuffle"))
                        {
                            MusicPlayer.settings.setMode("shuffle", false);
                            MusicPlayer.currentPlaylist = playlist;
                            MusicPlayer.settings.setMode("shuffle", true);
                        }
                        else
                        {
                            MusicPlayer.currentPlaylist = playlist;
                        }
                        if (listSongs.SelectedItems.Count == 1)
                        {
                            media = MusicPlayer.currentPlaylist.get_Item(index);
                        }
                        MusicPlayer.Ctlcontrols.playItem(media);
                    }
                    catch { }
                }
            }
        }

        private void MusicPlayer_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            int index = 0;
            for (int i = 0; i < MusicPlayer.currentPlaylist.count - 1; i++)
            {
                if (MusicPlayer.currentMedia.name == MusicPlayer.currentPlaylist.Item[i].name)
                {
                    index = i;
                    break;
                }
            }
            Song song = new Song();
            if (MusicPlayer.currentPlaylist.count == song_playlist.Count)
            {
                song = song_playlist.ElementAt(index);
                Artist artist = Artist.get(lib.artist_list, song.artist_id);
                //MessageBox.Show("No se ha podido reproducir: " + song.name + " - " + artist.name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                return;
            }
        }

        private void MusicPlayer_EndOfStream(object sender, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e)
        {
            lblSong_duration.Text = "00:00";
        }

        private void lblSong_album_Click(object sender, EventArgs e)
        {
            try
            {
                Artist artist = FindArtistByName(lblSong_Artist.Text);
                Album album = new Album();
                foreach (Album a in lib.album_list)
                {
                    if (a.name.ToLower() == lblSong_album.Text.ToLower() && a.artist_id == artist.id)
                    {
                        album = a;
                    }

                }
                SelectAlbum(artist, album);
            }
            catch
            {

            }
        }

        private void lblSong_Artist_Click(object sender, EventArgs e)
        {
            try
            {
                Artist artist = FindArtistByName(lblSong_Artist.Text);
                SelectArtist(artist);
            }
            catch { }
        }

        private void PANEL_Click(object sender, EventArgs e)
        {
            listAlbums.Items.Clear();
            listSongs.Items.Clear();
            listAlbums.SelectedIndex = -1;
            listSongs.SelectedIndex = -1;
            listArtists.SelectedIndex = -1;
            foreach(int i in user.artist_list)
            {
                Artist artist = Artist.get(lib.artist_list, i);
                foreach(Album album in lib.album_list)
                {
                    if(album.artist_id == artist.id)
                    {
                        ListBoxItem itemAlbum = new ListBoxItem();
                        itemAlbum.Text = album.name;
                        itemAlbum.Value = album;
                        listAlbums.Items.Add(itemAlbum);
                        foreach(Song song in lib.song_list)
                        {
                            if(song.album_id == album.id && song.artist_id == artist.id)
                            {
                                ListBoxItem itemSong = new ListBoxItem();
                                itemSong.Text = song.name;
                                itemSong.Value = song;
                                listSongs.Items.Add(itemSong);
                            }
                        }
                    }
                }
            }
        }
    }
}
