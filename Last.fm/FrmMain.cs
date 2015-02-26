using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using Last.fm.Properties;
using Settings;

namespace Last.fm
{

    public partial class FrmMain : Form
    {
        BinaryFormatter bf;
        List<string> AutoFill = new List<string>();
        string sessionKey = "empty";
        string logFile = "log.txt";
        string defFile = "settings.alr";
        string ApiKey = "ea13f7fe78f7ef62bbbc5c43dcd6dfcb";
        string mySecret = "aeaa72cd88242eb7f1437ed8b9937e16";
        string username = String.Empty;

        /// <summary>
        /// Список расширений, с которыми файлы добавляются через перетаскивание
        /// </summary>
        string[] allowedExtension = { 
                                        ".mp3", 
                                        ".wav", 
                                        ".flac", 
                                        ".ogg" 
                                    };

        #region Вспомогательные методы
        public bool Compare(int s1, int s2)
        {
            if ((s1 / 10) == (s2 / 10))
			{
                return ((s1 % 10) > (s2 % 10));
			}
            else if (s1 < 10)
			{
                return ((s1 % 10) >= (s2 / 10));
			}
            else if (s2 < 10)
			{
                return ((s1 / 10) > (s2 % 10));// в этом случае равно не нужно, ибо передвигать не нужно будет;
			}
            else 
			{
				return false;
			}
        }

        public int[] NeedArray(int n) // изменить сортировку
        {
            int[] tmp = new int[n];
            for (int i = 0; i < n; i++)
            {
                tmp[i] = i;
            }
            int s = 0;
            int h = 1;
            for (int i = 0; i < tmp.Length; i++)
            {
                for (int j = 0; j < tmp.Length - 1; j++)
                {
                    if (tmp[j] != 0)
                        if (Compare(tmp[j], tmp[j + 1]))
                        {
                            s = tmp[j];
                            tmp[j] = tmp[j + 1];
                            tmp[j + 1] = s;
                            h++;
                        }
                }
            }
            return tmp;
        }

        /// <summary>
        /// MD5 хеширование
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        string MD5(string s)
        {
            //переводим строку в байт-массим   
            byte[] bytes = Encoding.UTF8.GetBytes(s); // для ласта нужна ASCII (?)

            //создаем объект для получения средст шифрования   
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах   
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива   
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }

            return hash;
        }
        #endregion

        public FrmMain(string[] ardgs)
        {
            InitializeComponent();
            bf = new BinaryFormatter();

            cbTimeType.SelectedIndex = 0;

        }

        /// <summary>
        /// Получение ключа сессии
        /// </summary>
        /// <returns></returns>
        string GetSessionKey()
        {
            if (sessionKey == "empty")
            {
                return null;
            }
            
            return sessionKey;
        }

        void ScrobbleTracks(List<Song> T, DateTime start)
        {
            // делаем массив для правильной альфа-бета сортировки параметров
            int cnt = T.Count;
            int[] N = NeedArray(cnt); // получение отсортированного массивы для правильной сигнатуры


            string sessK = GetSessionKey();

            if (String.IsNullOrEmpty(sessK))
            {
                DialogResult f = MessageBox.Show("Вы должны предоставить приложению доступ к своему профилю. Сделать это сейчас?", "Ошибка", MessageBoxButtons.YesNo);
                if (f == DialogResult.Yes)
                {
                    mGiveAccess_Click(null, EventArgs.Empty);
                }
                else return;
            }

            sessK = GetSessionKey();

            // установка сеанса работы с сервером
            TimeSpan rtime = start - (new DateTime(1970, 1, 1, 0, 0, 0));
            TimeSpan t1 = new TimeSpan(3, 0, 0);
            rtime -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
            int timestamp = (int)rtime.TotalSeconds; // общее количество секунд

            int[] times = new int[50];
            for (int i = 0; i < times.Length; i++)
            {
                times[i] = timestamp - i * 300;
            }

            // формирование строки запроса
            string submissionReqString = String.Empty;

            // отдельное формирование сигнатуры и параметров
            //
            //параметры:
            submissionReqString += "method=track.scrobble&sk=" + sessK + "&api_key=" + ApiKey;

            for (int i = 0; i < cnt; i++)
            {
                // кодирование, чтобы спецсимволы правильно воспринимались
                submissionReqString += "&artist[" + i + "]=" + HttpUtility.UrlEncode(T[i].Artist);
                submissionReqString += "&track[" + i + "]=" + HttpUtility.UrlEncode(T[i].Track);
                submissionReqString += "&timestamp[" + i + "]=" + times[i].ToString();
                //if (String.IsNullOrEmpty(T[i].Album))
                // submissionReqString += "&album[" + i + "]=" + HttpUtility.UrlEncode(String.Empty);
                //else 
                submissionReqString += "&album[" + i + "]=" + HttpUtility.UrlEncode(T[i].Album);
                //timestamp -= 300;
            }

            //
            // сигнатура:
            string signature = String.Empty;


            for (int i = 0; i < cnt; i++)
            {
                signature += "album[" + N[i].ToString() + "]" + T[N[i]].Album;
            }

            signature += "api_key" + ApiKey;
            //
            // для сигнатуры нельзя делать Url кодирование, это сбивает запрос на 403 ошибку!
            // !!!все параметры нужно в альфа бета порядке делать!!!!. Здесь он выдавал не в той последовательности, время и параметры не совпадали.
            for (int i = 0; i < cnt; i++)
            {
                signature += "artist[" + N[i].ToString() + "]" + T[N[i]].Artist; // соответствие кэфов
            }
            signature += "methodtrack.scrobblesk" + sessK;

            for (int i = 0; i < cnt; i++)
            {
                signature += "timestamp[" + N[i].ToString() + "]" + times[N[i]].ToString();
            }

            for (int i = 0; i < cnt; i++)
            {
                signature += "track[" + N[i].ToString() + "]" + T[N[i]].Track;
            }

            //
            signature += mySecret; // добавляем секрет в конец
            submissionReqString += "&api_sig=" + MD5(signature); // добавляем сигнатуру к общему запросу
            //
            HttpWebRequest submissionRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/"); // адрес запроса (без параметров)
            submissionRequest.ServicePoint.Expect100Continue = false; // важная строка! из-за её отсутствия сервер ласта рвал соединение

            // Настраиваем параметры запроса
            submissionRequest.UserAgent = "Mozilla/5.0";
            submissionRequest.Method = "POST"; // Указываем метод отправки данных скрипту, в случае с Post обязательно
            submissionRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные
            submissionRequest.Timeout = 6000; // 
            //
            // Преобразуем данные к соответствующую кодировку
            byte[] EncodedPostParams = Encoding.UTF8.GetBytes(submissionReqString); // получение массива байтов из строки с параметрами (UTF8 обязательно)
            submissionRequest.ContentLength = EncodedPostParams.Length;
            //
            // Записываем данные в поток
            submissionRequest.GetRequestStream().Write(EncodedPostParams, 0,
                                            EncodedPostParams.Length); // запись в поток запроса (массив байтов, хз, сколько запиливаем)
            //

            //
            HttpWebResponse submissionResponse = (HttpWebResponse)submissionRequest.GetResponse(); // получаем ответ
            //
            // Получаем html-код страницы
            string submissionResult = new StreamReader(submissionResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd(); // считываем поток ответа
            //
            // разбор полётов
            if (!submissionResult.Contains("status=\"ok\"")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
                throw new Exception("Треки не отправлены! Причина - " + submissionResult);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // проверка на "пробельные" значения
            if (String.IsNullOrEmpty(tbArtist.Text) || (String.IsNullOrEmpty(tbTrack.Text)))
            {
                MessageBox.Show("Не заполнены необходимые поля!");
                return;
            }
            string q1 = tbArtist.Text;
            string q2 = tbTrack.Text;
            Song tmp = new Song { Artist = tbArtist.Text, Track = tbTrack.Text, Album = tbAlbum.Text };
            lbList.Items.Add(tmp);
            if (mSaveArtists.Checked)
            {
                AutoFill.Add(tbArtist.Text);
                AutoFill = AutoFill.Distinct().ToList(); // убираем дубликаты
                tbArtist.DataSource = null;
                tbArtist.DataSource = AutoFill;
                tbArtist.PerformLayout();
                tbArtist.Text = q1;
                tbTrack.Text = q2;
            }
            UpdateCountLabel();


        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (lbList.Items.Count == 0)
            {
                MessageBox.Show("Не добавлено ни одного трека!");
                return;
            }
            //

            try
            {
                List<Song> tmp = new List<Song>();
                foreach (var T in lbList.Items)
                {
                    tmp.Add((Song)T); // сливаем все песни в наш список
                }

                DateTime start = DateTime.Now;

                if (chStartTime.Checked)
                    start = dtStart.Value;

                // проверка даты!

                //
                int iterations = 1;
                if (tmp.Count > 50) // расчитываем количество итераций
                {
                    if (tmp.Count % 50 > 0)
                        iterations = tmp.Count / 50 + 1;
                    else iterations = tmp.Count / 50;
                }

                for (int i = 0; i < iterations; i++)
                {
                    //
                    ScrobbleTracks(GetButch(50 * i, 50, tmp), start); // отправляем по 50 треков
                    //
                    if ((i + 1) != iterations)
                        Thread.Sleep(1500); // ставим ожидание, чтобы дать серверу время на обработку
                    //
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (mLog.Checked)
                {
                    SaveLog(false);
                }
                return;
                // если метод не завершить принудительно, всё, что после catch - сработает
            }

            if (mLog.Checked)
            {
                SaveLog(true);
            }

            if (mClear.Checked) // отчищаем, если задано
            {
                lbList.Items.Clear();
                UpdateCountLabel();
            }

            MessageBox.Show("Треки отправлены!");
        }

        void LoadConfig()
        {
            FileStream f = new FileStream(defFile, FileMode.Open);
            ApplicationSettings tmp = (ApplicationSettings)bf.Deserialize(f);
            AutoFill = tmp.AutoFill; // переброс ссылки, tmp будет уничтожен, но ссылка на этот лист будет жить
            sessionKey = tmp.SessionKey;
            mLog.Checked = tmp.Log;
            mClear.Checked = tmp.Clear;
            tbArtist.DataSource = AutoFill;
            mSaveArtists.Checked = tmp.SaveArtists;
            mAutoClear.Checked = tmp.AutoClear;
            mAutoSave.Checked = tmp.AutoSave;
            mExitConfirm.Checked = tmp.ExitConfirm;
            f.Close();
        }

        public List<Song> GetButch(int start, int count, List<Song> t)
        {
            if (count + start > t.Count)
            {
                return t.GetRange(start, t.Count - start);
            }
            else
            {
                return t.GetRange(start, count);
            }
        }

        private void tbTrack_Enter(object sender, EventArgs e)
        {
            tbTrack.SelectAll();
        }

        private void tbArtist_Enter(object sender, EventArgs e)
        {
            tbArtist.SelectAll();
        }

        public bool AddOneLine(string s)
        {
            string[] separ = { " - " };
            string[] result = s.Split(separ, StringSplitOptions.None);
            if (result.Length == 2)
            {
                lbList.Items.Add(new Song { Artist = result[0], Track = result[1], Album = String.Empty });
                return true;
            }
            else if (result.Length == 3)
            {
                lbList.Items.Add(new Song { Artist = result[0], Track = result[1], Album = result[2] });
                return true;
            }
            else return false;
        }

        private void btnOneLine_Click(object sender, EventArgs e)
        {
            if (tbOneLine.Text.Length > 4)
                if (AddOneLine(tbOneLine.Text))
                {
                    UpdateCountLabel();
                }
        }

        private void DeleteMS_Click(object sender, EventArgs e)
        {
            lbList.Items.RemoveAt(lbList.SelectedIndex);
            UpdateCountLabel();
        }

        private void ClearMS_Click(object sender, EventArgs e)
        {
            lbList.Items.Clear();
            lblSongCount.Text = "0";
        }

        public bool IsSetUsername()
        {
            if (String.IsNullOrEmpty(username))
            {
                DialogResult f = MessageBox.Show("Для выполнения этой операции вам нужно ввести имя вашей учётной записи. Сделать это сейчас?", "Ошибка", MessageBoxButtons.YesNo);
                if (f == DialogResult.Yes)
                {
                    FrmSetUsername tmp = new FrmSetUsername();
                    if (DialogResult.OK == tmp.ShowDialog())
                    {
                        username = Program.username;
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }

                else return false;
            }

            else
            {
                return true;
            }
        }

        private void ArtistMS_Click(object sender, EventArgs e)
        {
            Song tmp = (Song)lbList.SelectedItem;
            Process.Start("http://last.fm/music/" + tmp.Artist);
        }

        private void SongMS_Click(object sender, EventArgs e)
        {
            Song tmp = (Song)lbList.SelectedItem;
            Process.Start("http://last.fm/music/" + tmp.Artist + "/_/" + tmp.Track);

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(defFile))
                {
                    LoadConfig();
                    UpdateAccessIcon();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        /// <summary>
        /// Обновление иконки доступа приложения к профилю
        /// </summary>
        void UpdateAccessIcon()
        {
            if (String.IsNullOrEmpty(GetSessionKey()))
            {
                pbAccess.Image = Resources.no;
                ttMain.SetToolTip(pbAccess, "Программе не дан доступ к профилю");
            }
            else
            {
                pbAccess.Image = Resources.ok;
                ttMain.SetToolTip(pbAccess, "Программе дан доступ к профилю");
            }
        }

        void SaveConfig()
        {
            FileStream g = new FileStream(defFile, FileMode.OpenOrCreate);
            ApplicationSettings tmp = new ApplicationSettings(username, sessionKey, mLog.Checked, mClear.Checked, mSaveArtists.Checked, mAutoSave.Checked, mAutoClear.Checked, mExitConfirm.Checked);
            if (AutoFill != null && AutoFill.Count > 0) // если есть что добавлять
                tmp.AutoFill.AddRange(AutoFill); // добавление добавленных с начала сеанса
            //tmp.AutoFill.AddRange((IEnumerable<string>)tbArtist.DataSource); //  
            //HashSet

            bf.Serialize(g, tmp);
            g.Close();
        }

        private void mSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void mDeleteData_Click(object sender, EventArgs e)
        {
            try
            {
                ClearConfig();
                UpdateAccessIcon();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        void ClearConfig()
        {
            try
            {
                if (File.Exists(defFile))
                {
                    FileStream g = new FileStream(defFile, FileMode.OpenOrCreate);
                    ApplicationSettings tmp = new ApplicationSettings(String.Empty, String.Empty, false, false, false, false, false, true);
                    tmp.AutoFill.Clear();
                    bf.Serialize(g, tmp);
                    sessionKey = String.Empty;
                    g.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void mchbClear_Click(object sender, EventArgs e)
        {
            mClear.Checked = !mClear.Checked;
        }

        private void mExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (mExitConfirm.Checked)
                {
                    if (MessageBox.Show("Уверены?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        e.Cancel = true; // отменяем закрытие программы
                        return;
                    }
                }
                if (mAutoClear.Checked)
                    ClearConfig();
                else if (mAutoSave.Checked)
                    SaveConfig();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void mProfile_Click(object sender, EventArgs e)
        {
            if (IsSetUsername())
                Process.Start("http://lastfm.ru/user/" + username);
        }

        public void GiveAccess()
        {
            try
            {
                XmlDocument xmlP = new XmlDocument();

                HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.gettoken&api_key=" + ApiKey); // получение токена
                HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
                string tokenResult = new StreamReader(tokenResponse.GetResponseStream(),
                                               Encoding.UTF8).ReadToEnd();

                // извлечение токена
                xmlP.LoadXml(tokenResult);
                string token = xmlP.SelectSingleNode("lfm/token").InnerText;

                // запрос на юзание, изменить на хранимую сессию
                Process s = Process.Start("http://www.last.fm/api/auth/?api_key=" + ApiKey + "&token=" + token);

                // делаем сигнатуру
                DialogResult d = MessageBox.Show("Вы подтвердили доступ?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (d == DialogResult.OK)
                {
                    string tmp = "api_key" + ApiKey + "methodauth.getsessiontoken" + token + mySecret; // сигнатура для получения сессии! (для каждого апиметода она разная)
                    string sig = MD5(tmp); // хеширование 


                    // получение сессии
                    HttpWebRequest sessionRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.getsession&token=" + token + "&api_key=" + ApiKey + "&api_sig=" + sig);
                    sessionRequest.AllowAutoRedirect = true;
                    HttpWebResponse sessionResponse = (HttpWebResponse)sessionRequest.GetResponse();
                    string sessionResult = new StreamReader(sessionResponse.GetResponseStream(),
                                                   Encoding.UTF8).ReadToEnd();
                    // извлечение сессии
                    xmlP.LoadXml(sessionResult);
                    sessionKey = xmlP.SelectSingleNode("lfm/session/key").InnerText;
                    UpdateAccessIcon();

                }
                else
                {
                    throw new Exception("Вы должны предоставить доступ приложению!");
                }
            }
            catch (Exception e1)
            {
                throw e1; // бросаем исключение "выше".
            }
            

        }

        private void mGiveAccess_Click(object sender, EventArgs e)
        {
            try
            {
                GiveAccess();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

        }

        private void mMassiveAdd_Click(object sender, EventArgs e)
        {
            FrmBigList f = new FrmBigList();
            f.Show();
            f.BringToFront();
        }

        private void ArtistLibraryMS_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSetUsername())
                {

                    Song tmp = (Song)lbList.SelectedItem;
                    Process.Start("http://last.fm/user/" + username + "/library/music/" + tmp.Artist);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void mAbout_Click(object sender, EventArgs e)
        {

            if (DialogResult.OK == MessageBox.Show(Resources.Info, "О программе", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                Process.Start("http://vk.com/note4223988_10790267");
            }
        }

        private void mGetTracks_Click(object sender, EventArgs e)
        {
            FrmExport frmEx = new FrmExport(username);
            frmEx.Show();
        }

        public void SaveLog(bool isComplete)
        {
            StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8);
            sw.WriteLine(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + ". Статус - " + ((isComplete) ? "Успешно" : "Не успешно"));

            for (int i = 0; i < lbList.Items.Count; i++)
            {
                Song tmp = (Song)lbList.Items[i];
                sw.WriteLine(tmp.ToString());
            }
            sw.WriteLine(Environment.NewLine);
            sw.Close();
        }

        private void mClear_Click(object sender, EventArgs e)
        {
            mClear.Checked = !mClear.Checked;
        }

        private void mLog_Click(object sender, EventArgs e)
        {
            mLog.Checked = !mLog.Checked;
        }

        private void mSaveArtists_Click(object sender, EventArgs e)
        {
            mSaveArtists.Checked = !mSaveArtists.Checked;
        }

        private void mClearArtists_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Уверены?", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AutoFill.Clear();
                SaveConfig();
                tbArtist.DataSource = null;
                tbArtist.DataSource = AutoFill;// перепривязываем источник данных
                tbArtist.PerformLayout();
            }
        }

        private void mAutoSave_Click(object sender, EventArgs e)
        {
            mAutoClear.Checked = false;
            mAutoSave.Checked = !mAutoSave.Checked;
        }

        private void mAutoClear_Click(object sender, EventArgs e)
        {
            mAutoSave.Checked = false;
            mAutoClear.Checked = !mAutoClear.Checked;
        }

        private void mOpenLog_Click(object sender, EventArgs e)
        {
            if (File.Exists(logFile))
            {
                Process.Start(logFile);
            }
            else MessageBox.Show("Лог отсутствует!", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void mWithAlbum_CheckedChanged(object sender, EventArgs e)
        {
            chStartTime.Checked = dtStart.Visible = tbAlbum.Visible = lblAlbum.Visible = mWithAlbum.Checked;
        }

        private void chStartTime_CheckedChanged(object sender, EventArgs e)
        {
            dtStart.Enabled = chStartTime.Checked;
        }

        public void AddFolder(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dI = new DirectoryInfo(path);
                FileInfo[] fI = dI.GetFiles();

                for (int i = 0; i < fI.Length; i++)
                {
                    AddFile(fI[i]);
                }
                UpdateCountLabel();
            }
            else throw new Exception ("Директория не существует");
        }

        
        public void AddFile(FileInfo path)
        {
            if (path.Exists)
            {
                if (CheckExtension(path.Extension)) // проверяем расширение
                {
                    AddOneLine(Path.GetFileNameWithoutExtension(path.FullName)); // добавляем без расширения
                }
            }
            else throw new Exception("Файл не существует");
        }

        public bool CheckExtension(string ext)
        {
            return allowedExtension.Contains(ext);
        }

        private void cbTimeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTimeType.SelectedIndex == 0)
            {
                dtStart.Format = DateTimePickerFormat.Custom;
                dtStart.ShowUpDown = false;
            }

            else
            {
                dtStart.Format = DateTimePickerFormat.Time;
                dtStart.ShowUpDown = true;
            }
        }

        private void mSetUserName_Click(object sender, EventArgs e)
        {
            FrmSetUsername tmp = new FrmSetUsername();
            if (DialogResult.OK == tmp.ShowDialog())
            {
                username = Program.username;
            }
        }

        private void cMSList_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cMSList.Enabled = (lbList.SelectedIndex < 0) ? false : true;
        }

        private void mEdit_Click(object sender, EventArgs e)
        {
            Song tmp = (Song)lbList.SelectedItem;
            tbArtist.Text = tmp.Artist;
            tbTrack.Text = tmp.Track;
            tbAlbum.Text = tmp.Album;
            lbList.Items.RemoveAt(lbList.SelectedIndex);
            UpdateCountLabel();

        }

        /// <summary>
        /// Обновление количества песен
        /// </summary>
        public void UpdateCountLabel()
        {
            lblSongCount.Text = lbList.Items.Count.ToString();
        }

        private void mExitConfirm_Click(object sender, EventArgs e)
        {
            mExitConfirm.Checked = !mExitConfirm.Checked;
        }

        private void tbAlbum_Enter(object sender, EventArgs e)
        {
            tbAlbum.SelectAll();
        }

        private void lbList_DragEnter(object sender, DragEventArgs e)
        {
            // Проверяем, подходят ли нам перетаскиваемые файлы
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Устанавливаем у курсора значок копирования
                return;            
            }
            
            e.Effect = DragDropEffects.None; // Устанавливаем у курсора значок запрета

        }

        private void lbList_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);

            for (int i = 0; i < FileList.Length; i++)
            {
                if (File.Exists(FileList[i])) // если это файл
                {
                    AddFile(new FileInfo(FileList[i]));
                }
                else if (Directory.Exists(FileList[i]))
                {
                    AddFolder(FileList[i]);
                }
            }
            UpdateCountLabel();


        }

        private void lbList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lbList.SelectedIndex > -1)
                {
                    int f = lbList.SelectedIndex; // получаем выбранный индекс 
                    lbList.Items.RemoveAt(f); // удаляем
                    if (f - 1 > -1) // если у нас не последний
                        lbList.SetSelected(f - 1, true); // выбираем предыдущий элемент
                    UpdateCountLabel();
                }
            }

            if (e.KeyCode == Keys.E)
            {
                if (lbList.SelectedIndex > -1)
                {
                    int f = lbList.SelectedIndex; // получаем выбранный индекс 
                    Song tmp = (Song)lbList.SelectedItem;
                    tbArtist.Text = tmp.Artist;
                    tbTrack.Text = tmp.Track;
                    tbAlbum.Text = tmp.Album;
                    lbList.Items.RemoveAt(lbList.SelectedIndex);
                    UpdateCountLabel();

                    if (f - 1 > -1) // если у нас не последний
                        lbList.SetSelected(f - 1, true); // выбираем предыдущий элемент
                }
            }
        }

        private void mSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.Desktop;
            if (DialogResult.OK == fd.ShowDialog())
            {
                AddFolder(fd.SelectedPath);
            }
        }

        private void mEnterPath_Click(object sender, EventArgs e)
        {

        }

        private void tbAddPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Directory.Exists(tbAddPath.Text))
                {
                    AddFolder(tbAddPath.Text);
                    // скрываем меню программно
                    for (int i = 0; i < mainMenu.Items.Count; i++)
                    {
                       ((ToolStripDropDownItem)mainMenu.Items[i]).HideDropDown();
                    }
                    tbAddPath.Text = String.Empty; // отчищаем текстовое поле
                }
            }
        }

        private void mVersionList_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.MainLink);
        }

        private void tbOneLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddOneLine(tbOneLine.Text);
            }
        }
    }
}


