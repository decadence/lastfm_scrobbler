namespace Last.fm
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lbList = new System.Windows.Forms.ListBox();
            this.cMSList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMS = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearMS = new System.Windows.Forms.ToolStripMenuItem();
            this.ArtistMS = new System.Windows.Forms.ToolStripMenuItem();
            this.ArtistLibraryMS = new System.Windows.Forms.ToolStripMenuItem();
            this.SongMS = new System.Windows.Forms.ToolStripMenuItem();
            this.lblList = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.tbAlbum = new System.Windows.Forms.TextBox();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.tbArtist = new System.Windows.Forms.ComboBox();
            this.pbLoved = new System.Windows.Forms.PictureBox();
            this.btnOneLine = new System.Windows.Forms.Button();
            this.tbOneLine = new System.Windows.Forms.TextBox();
            this.lblTrack = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.tbTrack = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.gbAuthorisation = new System.Windows.Forms.GroupBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lblSongCount = new System.Windows.Forms.Label();
            this.pBEqualiser = new System.Windows.Forms.PictureBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mOpenLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mMassiveAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mClearArtists = new System.Windows.Forms.ToolStripMenuItem();
            this.mProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.mGetTracks = new System.Windows.Forms.ToolStripMenuItem();
            this.mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mWithAlbum = new System.Windows.Forms.ToolStripMenuItem();
            this.mLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mSaveArtists = new System.Windows.Forms.ToolStripMenuItem();
            this.mAutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mAutoClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mData = new System.Windows.Forms.ToolStripMenuItem();
            this.mCheckLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mGiveAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mSaveData = new System.Windows.Forms.ToolStripMenuItem();
            this.mDeleteData = new System.Windows.Forms.ToolStripMenuItem();
            this.mSendingMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mLoginMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mAccessMode = new System.Windows.Forms.ToolStripMenuItem();
            this.cMSList.SuspendLayout();
            this.gbTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoved)).BeginInit();
            this.gbAuthorisation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBEqualiser)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbList
            // 
            this.lbList.ContextMenuStrip = this.cMSList;
            this.lbList.FormattingEnabled = true;
            this.lbList.HorizontalScrollbar = true;
            this.lbList.Location = new System.Drawing.Point(175, 29);
            this.lbList.Name = "lbList";
            this.lbList.Size = new System.Drawing.Size(183, 225);
            this.lbList.TabIndex = 8;
            // 
            // cMSList
            // 
            this.cMSList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMS,
            this.ClearMS,
            this.ArtistMS,
            this.ArtistLibraryMS,
            this.SongMS});
            this.cMSList.Name = "cMSList";
            this.cMSList.Size = new System.Drawing.Size(260, 114);
            // 
            // DeleteMS
            // 
            this.DeleteMS.Name = "DeleteMS";
            this.DeleteMS.Size = new System.Drawing.Size(259, 22);
            this.DeleteMS.Text = "Удалить";
            this.DeleteMS.Click += new System.EventHandler(this.DeleteMS_Click);
            // 
            // ClearMS
            // 
            this.ClearMS.Name = "ClearMS";
            this.ClearMS.Size = new System.Drawing.Size(259, 22);
            this.ClearMS.Text = "Отчистить";
            this.ClearMS.Click += new System.EventHandler(this.ClearMS_Click);
            // 
            // ArtistMS
            // 
            this.ArtistMS.Name = "ArtistMS";
            this.ArtistMS.Size = new System.Drawing.Size(259, 22);
            this.ArtistMS.Text = "Исполнитель";
            this.ArtistMS.Click += new System.EventHandler(this.ArtistMS_Click);
            // 
            // ArtistLibraryMS
            // 
            this.ArtistLibraryMS.Name = "ArtistLibraryMS";
            this.ArtistLibraryMS.Size = new System.Drawing.Size(259, 22);
            this.ArtistLibraryMS.Text = "Исполнитель в твоей библиотеке";
            this.ArtistLibraryMS.Click += new System.EventHandler(this.ArtistLibraryMS_Click);
            // 
            // SongMS
            // 
            this.SongMS.Name = "SongMS";
            this.SongMS.Size = new System.Drawing.Size(259, 22);
            this.SongMS.Text = "Песня";
            this.SongMS.Click += new System.EventHandler(this.SongMS_Click);
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Location = new System.Drawing.Point(173, 4);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(80, 13);
            this.lblList.TabIndex = 3;
            this.lblList.Text = "Список песен:";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.Location = new System.Drawing.Point(130, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gbTools
            // 
            this.gbTools.Controls.Add(this.tbAlbum);
            this.gbTools.Controls.Add(this.lblAlbum);
            this.gbTools.Controls.Add(this.tbArtist);
            this.gbTools.Controls.Add(this.pbLoved);
            this.gbTools.Controls.Add(this.btnOneLine);
            this.gbTools.Controls.Add(this.tbOneLine);
            this.gbTools.Controls.Add(this.lblTrack);
            this.gbTools.Controls.Add(this.lblArtist);
            this.gbTools.Controls.Add(this.tbTrack);
            this.gbTools.Controls.Add(this.btnAdd);
            this.gbTools.Location = new System.Drawing.Point(5, 99);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(161, 124);
            this.gbTools.TabIndex = 6;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Работа со списком песен:";
            // 
            // tbAlbum
            // 
            this.tbAlbum.Location = new System.Drawing.Point(47, 68);
            this.tbAlbum.Name = "tbAlbum";
            this.tbAlbum.Size = new System.Drawing.Size(80, 20);
            this.tbAlbum.TabIndex = 3;
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Location = new System.Drawing.Point(3, 70);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(46, 13);
            this.lblAlbum.TabIndex = 22;
            this.lblAlbum.Text = "Альбом";
            // 
            // tbArtist
            // 
            this.tbArtist.FormattingEnabled = true;
            this.tbArtist.Location = new System.Drawing.Point(47, 17);
            this.tbArtist.Name = "tbArtist";
            this.tbArtist.Size = new System.Drawing.Size(80, 21);
            this.tbArtist.TabIndex = 1;
            this.tbArtist.Enter += new System.EventHandler(this.tbTrack_Enter);
            // 
            // pbLoved
            // 
            this.pbLoved.Image = ((System.Drawing.Image)(resources.GetObject("pbLoved.Image")));
            this.pbLoved.Location = new System.Drawing.Point(134, 56);
            this.pbLoved.Name = "pbLoved";
            this.pbLoved.Size = new System.Drawing.Size(16, 16);
            this.pbLoved.TabIndex = 21;
            this.pbLoved.TabStop = false;
            this.pbLoved.Click += new System.EventHandler(this.pbLoved_Click);
            // 
            // btnOneLine
            // 
            this.btnOneLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnOneLine.Location = new System.Drawing.Point(132, 94);
            this.btnOneLine.Name = "btnOneLine";
            this.btnOneLine.Size = new System.Drawing.Size(25, 25);
            this.btnOneLine.TabIndex = 6;
            this.btnOneLine.Text = "+";
            this.btnOneLine.UseVisualStyleBackColor = true;
            this.btnOneLine.Click += new System.EventHandler(this.btnOneLine_Click);
            // 
            // tbOneLine
            // 
            this.tbOneLine.Location = new System.Drawing.Point(4, 97);
            this.tbOneLine.Name = "tbOneLine";
            this.tbOneLine.Size = new System.Drawing.Size(123, 20);
            this.tbOneLine.TabIndex = 5;
            this.tbOneLine.Text = "Example Artist - Example Track - Example Album";
            // 
            // lblTrack
            // 
            this.lblTrack.AutoSize = true;
            this.lblTrack.Location = new System.Drawing.Point(3, 45);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(32, 13);
            this.lblTrack.TabIndex = 8;
            this.lblTrack.Text = "Трек";
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(3, 20);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(42, 13);
            this.lblArtist.TabIndex = 7;
            this.lblArtist.Text = "Артист";
            // 
            // tbTrack
            // 
            this.tbTrack.Location = new System.Drawing.Point(47, 42);
            this.tbTrack.Name = "tbTrack";
            this.tbTrack.Size = new System.Drawing.Size(80, 20);
            this.tbTrack.TabIndex = 2;
            this.tbTrack.Enter += new System.EventHandler(this.tbTrack_Enter);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(5, 227);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(32, 23);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Go!";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // gbAuthorisation
            // 
            this.gbAuthorisation.Controls.Add(this.lblPassword);
            this.gbAuthorisation.Controls.Add(this.lblLogin);
            this.gbAuthorisation.Controls.Add(this.tbPassword);
            this.gbAuthorisation.Controls.Add(this.tbLogin);
            this.gbAuthorisation.Location = new System.Drawing.Point(5, 23);
            this.gbAuthorisation.Name = "gbAuthorisation";
            this.gbAuthorisation.Size = new System.Drawing.Size(161, 70);
            this.gbAuthorisation.TabIndex = 7;
            this.gbAuthorisation.TabStop = false;
            this.gbAuthorisation.Text = "Авторизация:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(2, 47);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(45, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Пароль";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(2, 21);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(38, 13);
            this.lblLogin.TabIndex = 3;
            this.lblLogin.Text = "Логин";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(47, 44);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(89, 20);
            this.tbPassword.TabIndex = 11;
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(47, 18);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(89, 20);
            this.tbLogin.TabIndex = 10;
            // 
            // lblSongCount
            // 
            this.lblSongCount.AutoSize = true;
            this.lblSongCount.Location = new System.Drawing.Point(138, 232);
            this.lblSongCount.Name = "lblSongCount";
            this.lblSongCount.Size = new System.Drawing.Size(13, 13);
            this.lblSongCount.TabIndex = 10;
            this.lblSongCount.Text = "0";
            // 
            // pBEqualiser
            // 
            this.pBEqualiser.Image = ((System.Drawing.Image)(resources.GetObject("pBEqualiser.Image")));
            this.pBEqualiser.Location = new System.Drawing.Point(158, 234);
            this.pBEqualiser.Name = "pBEqualiser";
            this.pBEqualiser.Size = new System.Drawing.Size(12, 12);
            this.pBEqualiser.TabIndex = 16;
            this.pBEqualiser.TabStop = false;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMain,
            this.mSettings,
            this.mData,
            this.mSendingMode});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(358, 24);
            this.mainMenu.TabIndex = 20;
            this.mainMenu.Text = "Меню";
            // 
            // mMain
            // 
            this.mMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpenLog,
            this.mMassiveAdd,
            this.mClearArtists,
            this.mProfile,
            this.mGetTracks,
            this.mAbout,
            this.mSeparator,
            this.mExit});
            this.mMain.Name = "mMain";
            this.mMain.Size = new System.Drawing.Size(68, 20);
            this.mMain.Text = "Функции";
            // 
            // mOpenLog
            // 
            this.mOpenLog.Name = "mOpenLog";
            this.mOpenLog.Size = new System.Drawing.Size(404, 22);
            this.mOpenLog.Text = "Открыть лог отправок";
            this.mOpenLog.Click += new System.EventHandler(this.mOpenLog_Click);
            // 
            // mMassiveAdd
            // 
            this.mMassiveAdd.Name = "mMassiveAdd";
            this.mMassiveAdd.Size = new System.Drawing.Size(404, 22);
            this.mMassiveAdd.Text = "Массовое добавление";
            this.mMassiveAdd.Click += new System.EventHandler(this.mMassiveAdd_Click);
            // 
            // mClearArtists
            // 
            this.mClearArtists.Name = "mClearArtists";
            this.mClearArtists.Size = new System.Drawing.Size(404, 22);
            this.mClearArtists.Text = "Отчистить список введённых исполнителей";
            this.mClearArtists.Click += new System.EventHandler(this.mClearArtists_Click);
            // 
            // mProfile
            // 
            this.mProfile.Name = "mProfile";
            this.mProfile.Size = new System.Drawing.Size(404, 22);
            this.mProfile.Text = "Перейти на страницу своего профиля";
            this.mProfile.Click += new System.EventHandler(this.mProfile_Click);
            // 
            // mGetTracks
            // 
            this.mGetTracks.Name = "mGetTracks";
            this.mGetTracks.Size = new System.Drawing.Size(404, 22);
            this.mGetTracks.Text = "Экспортировать список прослушанных композиций";
            this.mGetTracks.Click += new System.EventHandler(this.mGetTracks_Click);
            // 
            // mAbout
            // 
            this.mAbout.Name = "mAbout";
            this.mAbout.Size = new System.Drawing.Size(404, 22);
            this.mAbout.Text = "О программе/Обновление/Помощь/Сообщить об ошибке";
            this.mAbout.Click += new System.EventHandler(this.mAbout_Click);
            // 
            // mSeparator
            // 
            this.mSeparator.Name = "mSeparator";
            this.mSeparator.Size = new System.Drawing.Size(401, 6);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(404, 22);
            this.mExit.Text = "Выход";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // mSettings
            // 
            this.mSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mWithAlbum,
            this.mLog,
            this.mClear,
            this.mSaveArtists,
            this.mAutoSave,
            this.mAutoClear});
            this.mSettings.Name = "mSettings";
            this.mSettings.Size = new System.Drawing.Size(79, 20);
            this.mSettings.Text = "Настройки";
            // 
            // mWithAlbum
            // 
            this.mWithAlbum.Checked = true;
            this.mWithAlbum.CheckOnClick = true;
            this.mWithAlbum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mWithAlbum.Name = "mWithAlbum";
            this.mWithAlbum.Size = new System.Drawing.Size(290, 22);
            this.mWithAlbum.Text = "Расширенные данные при добавлении";
            this.mWithAlbum.Visible = false;
            this.mWithAlbum.CheckedChanged += new System.EventHandler(this.mWithAlbum_CheckedChanged);
            // 
            // mLog
            // 
            this.mLog.Name = "mLog";
            this.mLog.Size = new System.Drawing.Size(290, 22);
            this.mLog.Text = "Вести лог отправок";
            this.mLog.Click += new System.EventHandler(this.mLog_Click);
            // 
            // mClear
            // 
            this.mClear.Name = "mClear";
            this.mClear.Size = new System.Drawing.Size(290, 22);
            this.mClear.Text = "Отчищать при успешной отправке";
            this.mClear.Click += new System.EventHandler(this.mClear_Click);
            // 
            // mSaveArtists
            // 
            this.mSaveArtists.Name = "mSaveArtists";
            this.mSaveArtists.Size = new System.Drawing.Size(290, 22);
            this.mSaveArtists.Text = "Сохранять вводимых исполнителей";
            this.mSaveArtists.Click += new System.EventHandler(this.mSaveArtists_Click);
            // 
            // mAutoSave
            // 
            this.mAutoSave.Name = "mAutoSave";
            this.mAutoSave.Size = new System.Drawing.Size(290, 22);
            this.mAutoSave.Text = "Автосохранение настроек при выходе";
            this.mAutoSave.Click += new System.EventHandler(this.mAutoSave_Click);
            // 
            // mAutoClear
            // 
            this.mAutoClear.Name = "mAutoClear";
            this.mAutoClear.Size = new System.Drawing.Size(290, 22);
            this.mAutoClear.Text = "Автоотчистка настроек при выходе";
            this.mAutoClear.Click += new System.EventHandler(this.mAutoClear_Click);
            // 
            // mData
            // 
            this.mData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCheckLogin,
            this.mGiveAccess,
            this.toolStripSeparator1,
            this.mSaveData,
            this.mDeleteData});
            this.mData.Name = "mData";
            this.mData.Size = new System.Drawing.Size(62, 20);
            this.mData.Text = "Данные";
            // 
            // mCheckLogin
            // 
            this.mCheckLogin.Name = "mCheckLogin";
            this.mCheckLogin.Size = new System.Drawing.Size(282, 22);
            this.mCheckLogin.Text = "Проверить логин/пароль";
            this.mCheckLogin.Click += new System.EventHandler(this.mCheckLogin_Click);
            // 
            // mGiveAccess
            // 
            this.mGiveAccess.Name = "mGiveAccess";
            this.mGiveAccess.Size = new System.Drawing.Size(282, 22);
            this.mGiveAccess.Text = "Дать приложению доступ к профилю";
            this.mGiveAccess.Click += new System.EventHandler(this.mGiveAccess_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(279, 6);
            // 
            // mSaveData
            // 
            this.mSaveData.Name = "mSaveData";
            this.mSaveData.Size = new System.Drawing.Size(282, 22);
            this.mSaveData.Text = "Сохранить все настройки и данные";
            this.mSaveData.Click += new System.EventHandler(this.mSaveData_Click);
            // 
            // mDeleteData
            // 
            this.mDeleteData.Name = "mDeleteData";
            this.mDeleteData.Size = new System.Drawing.Size(282, 22);
            this.mDeleteData.Text = "Удалить все настройки и данные";
            this.mDeleteData.Click += new System.EventHandler(this.mDeleteData_Click);
            // 
            // mSendingMode
            // 
            this.mSendingMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLoginMode,
            this.mAccessMode});
            this.mSendingMode.Name = "mSendingMode";
            this.mSendingMode.Size = new System.Drawing.Size(111, 20);
            this.mSendingMode.Text = "Режим отправки";
            // 
            // mLoginMode
            // 
            this.mLoginMode.Checked = true;
            this.mLoginMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mLoginMode.Name = "mLoginMode";
            this.mLoginMode.ShowShortcutKeys = false;
            this.mLoginMode.Size = new System.Drawing.Size(200, 22);
            this.mLoginMode.Text = "Ввод логина/пароля";
            this.mLoginMode.CheckedChanged += new System.EventHandler(this.mMode_CheckedChanged);
            this.mLoginMode.Click += new System.EventHandler(this.mLoginMode_Click);
            // 
            // mAccessMode
            // 
            this.mAccessMode.Name = "mAccessMode";
            this.mAccessMode.ShowShortcutKeys = false;
            this.mAccessMode.Size = new System.Drawing.Size(200, 22);
            this.mAccessMode.Text = "Подтверждение доступа";
            this.mAccessMode.CheckedChanged += new System.EventHandler(this.mMode_CheckedChanged);
            this.mAccessMode.Click += new System.EventHandler(this.mAccessMode_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 251);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.gbAuthorisation);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblList);
            this.Controls.Add(this.pBEqualiser);
            this.Controls.Add(this.lbList);
            this.Controls.Add(this.gbTools);
            this.Controls.Add(this.lblSongCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Автономный Last.fm скробблер ";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.cMSList.ResumeLayout(false);
            this.gbTools.ResumeLayout(false);
            this.gbTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoved)).EndInit();
            this.gbAuthorisation.ResumeLayout(false);
            this.gbAuthorisation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBEqualiser)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gbTools;
        private System.Windows.Forms.TextBox tbTrack;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.GroupBox gbAuthorisation;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Button btnOneLine;
        private System.Windows.Forms.TextBox tbOneLine;
        private System.Windows.Forms.ContextMenuStrip cMSList;
        private System.Windows.Forms.ToolStripMenuItem DeleteMS;
        private System.Windows.Forms.ToolStripMenuItem ClearMS;
        private System.Windows.Forms.ToolStripMenuItem ArtistMS;
        private System.Windows.Forms.ToolStripMenuItem SongMS;
        private System.Windows.Forms.PictureBox pBEqualiser;
        public System.Windows.Forms.Label lblSongCount;
        public System.Windows.Forms.ListBox lbList;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mMain;
        private System.Windows.Forms.ToolStripMenuItem mData;
        private System.Windows.Forms.ToolStripMenuItem mSaveData;
        private System.Windows.Forms.ToolStripMenuItem mDeleteData;
        private System.Windows.Forms.ToolStripMenuItem mSendingMode;
        private System.Windows.Forms.ToolStripMenuItem mLoginMode;
        private System.Windows.Forms.ToolStripMenuItem mAccessMode;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.ToolStripMenuItem mProfile;
        private System.Windows.Forms.ToolStripSeparator mSeparator;
        private System.Windows.Forms.ToolStripMenuItem ArtistLibraryMS;
        private System.Windows.Forms.ToolStripMenuItem mAbout;
        private System.Windows.Forms.PictureBox pbLoved;
        private System.Windows.Forms.ToolStripMenuItem mGetTracks;
        private System.Windows.Forms.ToolStripMenuItem mCheckLogin;
        private System.Windows.Forms.ToolStripMenuItem mGiveAccess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mMassiveAdd;
        private System.Windows.Forms.ToolStripMenuItem mSettings;
        private System.Windows.Forms.ToolStripMenuItem mLog;
        private System.Windows.Forms.ToolStripMenuItem mClear;
        private System.Windows.Forms.ComboBox tbArtist;
        private System.Windows.Forms.ToolStripMenuItem mSaveArtists;
        private System.Windows.Forms.ToolStripMenuItem mClearArtists;
        private System.Windows.Forms.ToolStripMenuItem mAutoSave;
        private System.Windows.Forms.ToolStripMenuItem mAutoClear;
        private System.Windows.Forms.ToolStripMenuItem mOpenLog;
        private System.Windows.Forms.ToolStripMenuItem mWithAlbum;
        private System.Windows.Forms.TextBox tbAlbum;
        private System.Windows.Forms.Label lblAlbum;
    }
}

