using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using migh.api;
using System.Net;
using HtmlAgilityPack;
using System.Web;

namespace migh.player
{
    public partial class frmSongStatus : Form
    {
        public frmSongStatus()
        {
            InitializeComponent();
        }

        public string AudioFileURLFormat = "";
        public string FolderCheckFormat = "";

        List<string> Folders = new List<string>();
        public User user = new User();
        public Library lib = new Library();
        public List<SongStatusItem> SongStatusList = new List<SongStatusItem>();

        public class SongStatusItem
        {
            public Song song = new Song();
            public Album album = new Album();
            public Artist artist = new Artist();
            public string url { get; set; }
            public bool online = false;
        }
        private void frmSongStatus_Load(object sender, EventArgs e)
        {
            CreateFolders();
            CreateSongStatusItems();
            CheckSongStatus();
        }

        private void CheckSongStatus()
        {
            BackgroundWorker worker = new BackgroundWorker();
            frmSongURL frm = new frmSongURL();
            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                List<string> temp_urls = new List<string>();
                foreach (string Folder_url in Folders)
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    try
                    {
                        string page = webClient.DownloadString(Folder_url);

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(page);
                        foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='css-truncate css-truncate-target']").Where(d => d.InnerText.Contains(".m4a")))
                        {
                            foreach (HtmlNode row in node.Descendants().Where(d => d.Attributes.Contains("href") && d.Attributes.Contains("id")))
                            {
                                try
                                {
                                    string link = row.Attributes["href"].Value + "?raw=true";
                                    if (!link.Contains("https://github.com"))
                                    {
                                        link = "https://github.com" + link;
                                    }
                                    link = HttpUtility.HtmlDecode(link);
                                    foreach(ReplaceText rt in lib.configuration.GitHubFile_TextToReplace_HTML_List)
                                    {
                                        link = link.Replace(rt.OriginalText, rt.NewText);
                                    }
                                    //link = link.Replace("%26", "&");
                                    temp_urls.Add(link);
                                    frm.URLs.Add(link);
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }
                foreach (SongStatusItem item in SongStatusList)
                {
                    foreach (string str in temp_urls)
                    {
                        if (str.ToLower() == item.url.ToLower())
                        {
                            item.online = true;
                        }
                    }
                }

                foreach(SongStatusItem item in SongStatusList)
                {
                    ListViewItem i = new ListViewItem();
                    i.Text = item.song.name;
                    i.SubItems.Add(item.album.name);
                    i.SubItems.Add(item.artist.name);
                    if(item.online)
                    {
                        i.SubItems.Add("Disponible");
                    }
                    else
                    {
                        i.SubItems.Add("No disponible");
                    }
                    i.SubItems.Add(item.url);
                    listSongStatus.Invoke((MethodInvoker)(() => listSongStatus.Items.Add(i)));
                }
            };
            worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
            {
                frm.Show();
            };
            worker.RunWorkerAsync();
        }

        private void CreateFolders()
        {
            foreach (int artist_id in user.artist_list)
            {
                Artist artist = Artist.get(lib.artist_list, artist_id);

                foreach (Album album in lib.album_list)
                {
                    if (album.artist_id == artist_id)
                    {
                        string folder_url = string.Format(FolderCheckFormat, artist.url_name, album.url_name);
                        Folders.Add(folder_url);
                    }
                }

            }
        }

        private void CreateSongStatusItems()
        {
            foreach(int artist_id in user.artist_list)
            {
                Artist artist = Artist.get(lib.artist_list, artist_id);

                foreach(Album album in lib.album_list)
                {
                    if(album.artist_id == artist_id)
                    {
                        foreach(Song song in lib.song_list)
                        {
                            if(song.album_id == album.id)
                            {
                                SongStatusItem item = new SongStatusItem();
                                item.song = song;
                                item.album = album;
                                item.artist = artist;

                                string song_url_name = Tools.ConvertToGitHubFile(song.file_name, lib.configuration.GitHubFile_TextToReplace_List);
                                item.url = string.Format(lib.configuration.AudioFileURLFormat, artist.url_name, album.url_name, song_url_name);
                                SongStatusList.Add(item);
                            }
                        }
                    }
                }
            }
        }

        private void listSongStatus_DoubleClick(object sender, EventArgs e)
        {
            if(listSongStatus.SelectedItems.Count == 1)
            {
                try
                {
                    Clipboard.SetText(listSongStatus.SelectedItems[0].SubItems[4].Text);
                }
                catch
                {
                    
                }
            }
        }
    }
}
