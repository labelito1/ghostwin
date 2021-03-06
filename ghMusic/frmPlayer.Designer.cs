﻿namespace migh.player
{
    partial class frmPlayer
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlayer));
            this.listArtists = new System.Windows.Forms.ListBox();
            this.listAlbums = new System.Windows.Forms.ListBox();
            this.listSongs = new System.Windows.Forms.ListBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Exitbtn = new System.Windows.Forms.Button();
            this.cbxSearch = new System.Windows.Forms.ComboBox();
            this.Minbtn = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.lblSong_Title = new System.Windows.Forms.Label();
            this.lblSong_Artist = new System.Windows.Forms.Label();
            this.lblSong_album = new System.Windows.Forms.Label();
            this.lblSong_duration = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.AlbumCover = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblLoading = new System.Windows.Forms.Label();
            this.PANEL = new System.Windows.Forms.Panel();
            this.MusicPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MusicPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // listArtists
            // 
            this.listArtists.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.listArtists.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listArtists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listArtists.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listArtists.FormattingEnabled = true;
            this.listArtists.HorizontalScrollbar = true;
            this.listArtists.Location = new System.Drawing.Point(0, 0);
            this.listArtists.Name = "listArtists";
            this.listArtists.Size = new System.Drawing.Size(151, 264);
            this.listArtists.TabIndex = 2;
            this.listArtists.SelectedIndexChanged += new System.EventHandler(this.listArtists_SelectedIndexChanged);
            // 
            // listAlbums
            // 
            this.listAlbums.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.listAlbums.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listAlbums.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAlbums.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listAlbums.FormattingEnabled = true;
            this.listAlbums.HorizontalScrollbar = true;
            this.listAlbums.Location = new System.Drawing.Point(0, 0);
            this.listAlbums.Name = "listAlbums";
            this.listAlbums.Size = new System.Drawing.Size(268, 264);
            this.listAlbums.TabIndex = 3;
            this.listAlbums.SelectedIndexChanged += new System.EventHandler(this.listAlbums_SelectedIndexChanged);
            this.listAlbums.DoubleClick += new System.EventHandler(this.listAlbums_DoubleClick);
            // 
            // listSongs
            // 
            this.listSongs.AllowDrop = true;
            this.listSongs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.listSongs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listSongs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSongs.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listSongs.FormattingEnabled = true;
            this.listSongs.HorizontalScrollbar = true;
            this.listSongs.Location = new System.Drawing.Point(0, 0);
            this.listSongs.Name = "listSongs";
            this.listSongs.Size = new System.Drawing.Size(494, 264);
            this.listSongs.TabIndex = 4;
            this.listSongs.DoubleClick += new System.EventHandler(this.listSongs_DoubleClick);
            this.listSongs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listSongs_KeyDown);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.TopPanel.Controls.Add(this.btnSettings);
            this.TopPanel.Controls.Add(this.lblTitle);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.Exitbtn);
            this.TopPanel.Controls.Add(this.cbxSearch);
            this.TopPanel.Controls.Add(this.Minbtn);
            this.TopPanel.Controls.Add(this.txtSearch);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(943, 25);
            this.TopPanel.TabIndex = 21;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBarPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleBarPanel_MouseUp);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Image = global::migh.player.Properties.Resources.Settings;
            this.btnSettings.Location = new System.Drawing.Point(832, 2);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(20, 20);
            this.btnSettings.TabIndex = 31;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Visible = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            this.btnSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSettings_MouseDown);
            this.btnSettings.MouseEnter += new System.EventHandler(this.btnSettings_MouseEnter);
            this.btnSettings.MouseLeave += new System.EventHandler(this.btnSettings_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.AllowDrop = true;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblTitle.Location = new System.Drawing.Point(3, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(68, 15);
            this.lblTitle.TabIndex = 102;
            this.lblTitle.Text = "Bienvenido";
            this.lblTitle.UseMnemonic = false;
            this.lblTitle.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblTitle_DragDrop);
            this.lblTitle.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblTitle_DragEnter);
            this.lblTitle.DoubleClick += new System.EventHandler(this.lblTitle_DoubleClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Enabled = false;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Image = global::migh.player.Properties.Resources.Search;
            this.label1.Location = new System.Drawing.Point(611, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 21);
            this.label1.TabIndex = 32;
            // 
            // Exitbtn
            // 
            this.Exitbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exitbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Exitbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Exitbtn.FlatAppearance.BorderSize = 0;
            this.Exitbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Exitbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exitbtn.Image = global::migh.player.Properties.Resources.close2;
            this.Exitbtn.Location = new System.Drawing.Point(896, 3);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(43, 18);
            this.Exitbtn.TabIndex = 101;
            this.Exitbtn.TabStop = false;
            this.Exitbtn.UseVisualStyleBackColor = true;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            this.Exitbtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Exitbtn_MouseDown);
            this.Exitbtn.MouseEnter += new System.EventHandler(this.Exitbtn_MouseEnter);
            this.Exitbtn.MouseLeave += new System.EventHandler(this.Exitbtn_MouseLeave);
            // 
            // cbxSearch
            // 
            this.cbxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSearch.BackColor = System.Drawing.SystemColors.Menu;
            this.cbxSearch.DropDownHeight = 400;
            this.cbxSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSearch.IntegralHeight = false;
            this.cbxSearch.Location = new System.Drawing.Point(416, 3);
            this.cbxSearch.MaxDropDownItems = 15;
            this.cbxSearch.Name = "cbxSearch";
            this.cbxSearch.Size = new System.Drawing.Size(436, 21);
            this.cbxSearch.TabIndex = 0;
            this.cbxSearch.Visible = false;
            this.cbxSearch.SelectedIndexChanged += new System.EventHandler(this.cbxSearch_SelectedIndexChanged);
            // 
            // Minbtn
            // 
            this.Minbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Minbtn.FlatAppearance.BorderSize = 0;
            this.Minbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Minbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Minbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Minbtn.Image = global::migh.player.Properties.Resources.min;
            this.Minbtn.Location = new System.Drawing.Point(858, 3);
            this.Minbtn.Name = "Minbtn";
            this.Minbtn.Size = new System.Drawing.Size(32, 18);
            this.Minbtn.TabIndex = 100;
            this.Minbtn.TabStop = false;
            this.Minbtn.UseVisualStyleBackColor = true;
            this.Minbtn.Click += new System.EventHandler(this.Minbtn_Click);
            this.Minbtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Minbtn_MouseDown);
            this.Minbtn.MouseEnter += new System.EventHandler(this.Minbtn_MouseEnter);
            this.Minbtn.MouseLeave += new System.EventHandler(this.Minbtn_MouseLeave);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtSearch.Location = new System.Drawing.Point(641, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(211, 13);
            this.txtSearch.TabIndex = 31;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // RightPanel
            // 
            this.RightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Enabled = false;
            this.RightPanel.Location = new System.Drawing.Point(939, 25);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(4, 477);
            this.RightPanel.TabIndex = 22;
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Enabled = false;
            this.LeftPanel.Location = new System.Drawing.Point(0, 25);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(4, 477);
            this.LeftPanel.TabIndex = 23;
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Enabled = false;
            this.BottomPanel.Location = new System.Drawing.Point(4, 498);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(935, 4);
            this.BottomPanel.TabIndex = 24;
            // 
            // lblSong_Title
            // 
            this.lblSong_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSong_Title.AutoSize = true;
            this.lblSong_Title.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblSong_Title.Location = new System.Drawing.Point(168, 346);
            this.lblSong_Title.Name = "lblSong_Title";
            this.lblSong_Title.Size = new System.Drawing.Size(35, 13);
            this.lblSong_Title.TabIndex = 25;
            this.lblSong_Title.Text = "Título";
            this.lblSong_Title.UseMnemonic = false;
            this.lblSong_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSong_Title_MouseDown);
            // 
            // lblSong_Artist
            // 
            this.lblSong_Artist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSong_Artist.AutoSize = true;
            this.lblSong_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSong_Artist.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblSong_Artist.Location = new System.Drawing.Point(168, 370);
            this.lblSong_Artist.Name = "lblSong_Artist";
            this.lblSong_Artist.Size = new System.Drawing.Size(47, 15);
            this.lblSong_Artist.TabIndex = 26;
            this.lblSong_Artist.Text = "Artista";
            this.lblSong_Artist.UseMnemonic = false;
            this.lblSong_Artist.Click += new System.EventHandler(this.lblSong_Artist_Click);
            this.lblSong_Artist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSong_Artist_MouseDown);
            // 
            // lblSong_album
            // 
            this.lblSong_album.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSong_album.AutoSize = true;
            this.lblSong_album.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblSong_album.Location = new System.Drawing.Point(168, 396);
            this.lblSong_album.Name = "lblSong_album";
            this.lblSong_album.Size = new System.Drawing.Size(36, 13);
            this.lblSong_album.TabIndex = 27;
            this.lblSong_album.Text = "Álbum";
            this.lblSong_album.UseMnemonic = false;
            this.lblSong_album.Click += new System.EventHandler(this.lblSong_album_Click);
            this.lblSong_album.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSong_album_MouseDown);
            // 
            // lblSong_duration
            // 
            this.lblSong_duration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSong_duration.AutoSize = true;
            this.lblSong_duration.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSong_duration.Location = new System.Drawing.Point(168, 438);
            this.lblSong_duration.Name = "lblSong_duration";
            this.lblSong_duration.Size = new System.Drawing.Size(50, 13);
            this.lblSong_duration.TabIndex = 28;
            this.lblSong_duration.Text = "Duración";
            this.lblSong_duration.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblSong_duration.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listAlbums);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 235;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listSongs);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 431;
            this.splitContainer1.Size = new System.Drawing.Size(766, 264);
            this.splitContainer1.SplitterDistance = 268;
            this.splitContainer1.TabIndex = 29;
            // 
            // btnRepeat
            // 
            this.btnRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRepeat.BackColor = System.Drawing.Color.Transparent;
            this.btnRepeat.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepeat.FlatAppearance.BorderSize = 0;
            this.btnRepeat.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepeat.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeat.Image = global::migh.player.Properties.Resources.Repeat_off;
            this.btnRepeat.Location = new System.Drawing.Point(167, 301);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(16, 16);
            this.btnRepeat.TabIndex = 30;
            this.btnRepeat.UseVisualStyleBackColor = false;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShuffle.BackColor = System.Drawing.Color.Transparent;
            this.btnShuffle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnShuffle.FlatAppearance.BorderSize = 0;
            this.btnShuffle.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnShuffle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShuffle.Image = global::migh.player.Properties.Resources.Shuffle_off;
            this.btnShuffle.Location = new System.Drawing.Point(198, 301);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(16, 16);
            this.btnShuffle.TabIndex = 5;
            this.btnShuffle.UseVisualStyleBackColor = false;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // AlbumCover
            // 
            this.AlbumCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AlbumCover.ErrorImage = ((System.Drawing.Image)(resources.GetObject("AlbumCover.ErrorImage")));
            this.AlbumCover.InitialImage = ((System.Drawing.Image)(resources.GetObject("AlbumCover.InitialImage")));
            this.AlbumCover.Location = new System.Drawing.Point(12, 301);
            this.AlbumCover.Name = "AlbumCover";
            this.AlbumCover.Size = new System.Drawing.Size(150, 150);
            this.AlbumCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AlbumCover.TabIndex = 18;
            this.AlbumCover.TabStop = false;
            this.AlbumCover.DoubleClick += new System.EventHandler(this.AlbumCover_DoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(12, 31);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listArtists);
            this.splitContainer2.Panel1MinSize = 150;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(919, 264);
            this.splitContainer2.SplitterDistance = 151;
            this.splitContainer2.TabIndex = 33;
            // 
            // lblLoading
            // 
            this.lblLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoading.AutoSize = true;
            this.lblLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblLoading.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblLoading.Location = new System.Drawing.Point(871, 466);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(62, 13);
            this.lblLoading.TabIndex = 34;
            this.lblLoading.Text = "Cargando...";
            this.lblLoading.Visible = false;
            // 
            // PANEL
            // 
            this.PANEL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PANEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.PANEL.Location = new System.Drawing.Point(4, 31);
            this.PANEL.Name = "PANEL";
            this.PANEL.Size = new System.Drawing.Size(7, 264);
            this.PANEL.TabIndex = 35;
            this.PANEL.Click += new System.EventHandler(this.PANEL_Click);
            // 
            // MusicPlayer
            // 
            this.MusicPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MusicPlayer.Enabled = true;
            this.MusicPlayer.Location = new System.Drawing.Point(3, 456);
            this.MusicPlayer.Name = "MusicPlayer";
            this.MusicPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MusicPlayer.OcxState")));
            this.MusicPlayer.Size = new System.Drawing.Size(940, 43);
            this.MusicPlayer.TabIndex = 15;
            this.MusicPlayer.TabStop = false;
            this.MusicPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MusicPlayer_PlayStateChange);
            this.MusicPlayer.EndOfStream += new AxWMPLib._WMPOCXEvents_EndOfStreamEventHandler(this.MusicPlayer_EndOfStream);
            this.MusicPlayer.CurrentItemChange += new AxWMPLib._WMPOCXEvents_CurrentItemChangeEventHandler(this.MusicPlayer_CurrentItemChange);
            this.MusicPlayer.MediaError += new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.MusicPlayer_MediaError);
            // 
            // frmPlayer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(943, 502);
            this.Controls.Add(this.PANEL);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.lblSong_duration);
            this.Controls.Add(this.lblSong_album);
            this.Controls.Add(this.lblSong_Artist);
            this.Controls.Add(this.lblSong_Title);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.AlbumCover);
            this.Controls.Add(this.MusicPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(847, 465);
            this.Name = "frmPlayer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "migh.player";
            this.Load += new System.EventHandler(this.frmPlayer_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AlbumCover)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MusicPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listArtists;
        private System.Windows.Forms.ListBox listAlbums;
        private System.Windows.Forms.ListBox listSongs;
        private AxWMPLib.AxWindowsMediaPlayer MusicPlayer;
        private System.Windows.Forms.PictureBox AlbumCover;
        private System.Windows.Forms.Button Minbtn;
        private System.Windows.Forms.Button Exitbtn;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Label lblSong_Title;
        private System.Windows.Forms.Label lblSong_Artist;
        private System.Windows.Forms.Label lblSong_album;
        private System.Windows.Forms.Label lblSong_duration;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Button btnSettings;
        public System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Panel PANEL;
    }
}

