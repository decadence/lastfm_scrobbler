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
using Settings;


// нет понятия глобального юзинга, то есть
//using System.Collections.Generic;
//using System.Collections;
// не дают одинакового эффекта, нижний юзинг не покрывает верхний

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


        #region Вспомогательные методы
        public bool Compare(int s1, int s2)
        {
            if ((s1 / 10) == (s2 / 10))
                return ((s1 % 10) > (s2 % 10));
            else if (s1 < 10)
                return ((s1 % 10) >= (s2 / 10));
            else if (s2 < 10)
                return ((s1 / 10) > (s2 % 10));// в этом случае равно не нужно, ибо передвигать не нужно будет;
            else return false;
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


        

        string MD5(string s) // MD5
        {
            //переводим строку в байт-массим   
            byte[] bytes = Encoding.UTF8.GetBytes(s); // для ласта нужна ASCII (?)

            //создаем объект для получения средст шифрования   
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах   
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива   
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
        #endregion

        public FrmMain(string[] ardgs)
        {
            InitializeComponent();
            bf = new BinaryFormatter();
            /*
            if (ardgs.Length > 0)
            {
               // string s = "";
               // foreach (string r in ardgs)
               //     s+=" " + r;
               // MessageBox.Show(s);
               tbLogin.Text = ardgs[0];
                tbPassword.Text = ardgs[1];
            }*/
        }

        string GetSessionKey()
        {
            if (sessionKey == "empty")
                return null;
            else return sessionKey;
        }

        void ScrobbleTracks(List<Song> T)
        {
            /*
            string[] m = new string[T.Count]; // создаём с запасом
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = i.ToString();
            }
            //m.OrderBy(x => x.ToString());
            Array.Sort(m);
            */

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
                    else
                    {
                        throw new Exception("Вы должны предоставить приложению доступ к своему профилю! Или выберите другой режим отправки треков.");
                    }
                        
                }

                sessK = GetSessionKey();

                // установка сеанса работы с сервером
                TimeSpan rtime = DateTime.Now - (new DateTime(1970, 1, 1, 0, 0, 0));
                TimeSpan t1 = new TimeSpan(3, 0, 0);
                rtime -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
                int timestamp = (int)rtime.TotalSeconds; // общее количество секунд

                int [] times = new int[50];
                for (int i = 0; i < times.Length; i++)
                {
                    times[i] = timestamp - i * 300;
                }

                // формирование строки запроса
                string submissionReqString = String.Empty;
                
                // отдельное формирование сигнатуры и параметров

                //параметры:
                submissionReqString += "method=track.scrobble&sk=" + sessK + "&api_key=" + ApiKey;
                
            /*
            for (int i = 0; i < cnt; i++)
                {
                    submissionReqString += "&artist[" + i + "]=" + HttpUtility.UrlEncode(T[i].Artist);
                }

                for (int i = 0; i < cnt; i++)
                {
                    submissionReqString += "&track[" + i + "]=" + HttpUtility.UrlEncode(T[i].Track);
                }
                
                for (int i = 0; i < cnt; i++)
                {
                    submissionReqString += "&timestamp[" + i + "]=" + timestamp.ToString();
                    timestamp -= 300;
                }
            */

                /*

                int[] t = { 0, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 1, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 2 };
                for (int i = 0; i < T.Count; i++)
                {
                    submissionReq += "&artist[" + i + "]=" + T[i].Artist;

                    submissionReq += "&track[" + i + "]=" + T[i].Track;
                    submissionReq += "&timestamp[" + i + "]=" + timestamp.ToString();
                    //timestamp -= 300;
                }*/

                for (int i = 0; i < cnt;  i++)
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


                // сигнатура:
                string signature = String.Empty;


                for (int i = 0; i < cnt; i++)
                {
                    signature += "album[" + N[i].ToString() + "]" + T[N[i]].Album;
                }

                signature += "api_key" + ApiKey;
                
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


                signature += mySecret; // добавляем секрет в конец
                submissionReqString += "&api_sig=" + MD5(signature); // добавляем сигнатуру к общему запросу

                HttpWebRequest submissionRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/"); // адрес запроса (без параметров)
                submissionRequest.ServicePoint.Expect100Continue = false; // важная строка! из-за её отсутствия сервер ласта рвал соединение
                
                // Настраиваем параметры запроса
                submissionRequest.UserAgent = "Mozilla/5.0";
                submissionRequest.Method = "POST"; // Указываем метод отправки данных скрипту, в случае с Post обязательно
                submissionRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные
                submissionRequest.Timeout = 6000; // 

                // Преобразуем данные к соответствующую кодировку
                byte[] EncodedPostParams = Encoding.UTF8.GetBytes(submissionReqString); // получение массива байтов из строки с параметрами (UTF8 обязательно)
                submissionRequest.ContentLength = EncodedPostParams.Length;

                // Записываем данные в поток
                submissionRequest.GetRequestStream().Write(EncodedPostParams, 0,
                                                EncodedPostParams.Length); // запись в поток запроса (массив байтов, хз, сколько запиливаем)

                submissionRequest.GetRequestStream().Close();

                
                
                HttpWebResponse submissionResponse = (HttpWebResponse)submissionRequest.GetResponse(); // получаем ответ

                // Получаем html-код страницы
                string submissionResult = new StreamReader(submissionResponse.GetResponseStream(),
                                               Encoding.UTF8).ReadToEnd(); // считываем поток ответа

                // разбор полётов
                if (!submissionResult.Contains("status=\"ok\"")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
                    throw new Exception("Треки не отправлены! Причина - " + submissionResult);
            
 
        }

        void ScrobbleTracks (string login, string password, List<Song> T)
        {
            // установка сеанса работы с сервером
            TimeSpan rtime = DateTime.Now - (new DateTime(1970, 1, 1, 0, 0, 0));
            TimeSpan t1 = new TimeSpan(3, 0, 0);
            rtime -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
            int timestamp = (int)rtime.TotalSeconds; // общее количество секунд
            string client_id = "tst"; // для тестовых клиентов используется этот идентификатор
            string client_ver = "1.0"; // и такая версия
            string auth = MD5(MD5(password) + timestamp);//токен авторизации, который равен md5(md5(пароль) + timestamp)
            string handshakeReq = "http://post.audioscrobbler.com/?hs=true&p=1.2.1&c=" + client_id + "&v= " + client_ver + "&u=" + login + "&t=" + timestamp + "&a=" + auth;// + pre); // создаём с адресом в параметрах.

            HttpWebRequest handshakerequest = (HttpWebRequest)WebRequest.Create(handshakeReq);
            handshakerequest.UserAgent = "Mozilla/5.0"; // 
            handshakerequest.Timeout = 10000; // установка таймаута для методов получения входного потока и потока ответа. Вылетит ошибка по истечении этого времени, а не зависнет программа. Нужно при недоступности узла или отсутствии подключения.
            handshakerequest.AllowAutoRedirect = true; // допустимо ли перенаправление
            HttpWebResponse handshakeResponse = (HttpWebResponse)handshakerequest.GetResponse(); // получение ответа от сервераа
            string handshakeResult = new StreamReader(handshakeResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd(); // чтение потока (поток ответа, кодировка).чтение потока до конца.
            ///////////////////////////////////

            // Формируем строку с параметрами
            // выбираем вторую строку с айди сессии
            string sessID = String.Empty;
            int d = 0;
            for (int i = 0; i < handshakeResult.Length; i++)
            {
                if (handshakeResult[i] == '\n')
                {
                    d++;
                    continue;
                }
                if (d > 1) break;
                if (d == 1) sessID += handshakeResult[i];
            }

            //string submissionReq = "s=" + HttpUtility.UrlEncode(sessID) + "&a[0]=" + HttpUtility.UrlEncode(artist) + "&t[0]=" + HttpUtility.UrlEncode(track) + "&i[0]=" + HttpUtility.UrlEncode(timestamp.ToString()) + "&o[0]=P&r[0]=L&l[0]=300&b[0]=" + String.Empty + "&n[0]=1&m[0]=" + String.Empty;
           // string submissoonReq2 = "s="+ HttpUtility.UrlEncode(sessID)+"&a[1]=" + HttpUtility.UrlEncode("Disturbed") + "&t[1]=" + HttpUtility.UrlEncode("Session") + "&i[1]=" + HttpUtility.UrlEncode(timestamp.ToString()) + "&o[1]=P&r[1]=L&l[1]=300&b[1]=" + String.Empty + "&n[1]=1&m[1]=" + String.Empty;
            //string submissionReq = "s=" + HttpUtility.UrlEncode(sessID) + "&a[0]=" + HttpUtility.UrlEncode(artist) + "&t[0]=" + HttpUtility.UrlEncode(track) + "&i[0]=" + HttpUtility.UrlEncode(timestamp.ToString()) + "&o[0]=P&r[0]=L&l[0]=300&b[0]=" + String.Empty + "&n[0]=1&m[0]=1" + String.Empty + "&a[1]=" + HttpUtility.UrlEncode("Disturbed") + "&t[1]=" + HttpUtility.UrlEncode("Session") + "&i[1]=" + HttpUtility.UrlEncode(timestamp.ToString()) + "&o[1]=P&r[1]=L&l[1]=300&b[1]=" + String.Empty + "&n[1]=1&m[1]=" + String.Empty;
            

            // формирование строки запроса
            string submissionReq = "s=" + sessID;
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&a[" + i + "]=" + HttpUtility.UrlEncode(T[i].Artist);
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&t[" + i + "]=" + HttpUtility.UrlEncode(T[i].Track);
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&i[" + i + "]=" + timestamp;
                timestamp -= 300;
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&o[" + i + "]=P";
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&r[" + i + "]=L";
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&l[" + i + "]=300";
            }
            for (int i = 0; i < T.Count; i++)
            {
                //if (String.IsNullOrEmpty(T[i].Album)) 
               // submissionReq += "&b[" + i + "]=" + String.Empty;
                //else 
                submissionReq += "&b[" + i + "]=" + HttpUtility.UrlEncode(T[i].Album);
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&n[" + i + "]=1";
            }
            for (int i = 0; i < T.Count; i++)
            {
                submissionReq += "&m[" + i + "]=" + String.Empty;
            }
            
            
            
            
            
           // string submissionReq = "s=" + sessID + "&a[0]=" + artist + "&a[1]=Disturbed&t[0]=" + track + "&t[1]=Session&i[0]=" + timestamp + "&i[1]=" + (timestamp+300) + "&o[0]=P&o[1]=P&r[0]=L&r[1]=L&l[0]=300&l[1]=300&b[0]=" + String.Empty + "&b[1]=" + String.Empty + "&n[0]=1&n[1]=1&m[0]=" + String.Empty + "&m[1]=" + String.Empty;

            HttpWebRequest submissionRequest = (HttpWebRequest)WebRequest.Create("http://post2.audioscrobbler.com:80/protocol_1.2"); // адрес запроса (без параметров)
           // HttpWebRequest submissionRequest2 = (HttpWebRequest)WebRequest.Create("http://post2.audioscrobbler.com:80/protocol_1.2"); // адрес запроса (без параметров)

            // Настраиваем параметры запроса
            submissionRequest.UserAgent = "Mozilla/5.0";
            submissionRequest.Method = "POST"; // Указываем метод отправки данных скрипту, в случае с Post обязательно


            submissionRequest.AllowAutoRedirect = true;
            
            // request.Referer = mainSiteUrl;
            // request.CookieContainer = cookieCont;

            // Указываем тип отправляемых данных
            submissionRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные

            // Преобразуем данные к соответствующую кодировку. 
            // Обязательно кодирование в UTF8! Иначе могут возникнуть проблемы с русскими названиями треков!
            byte[] EncodedPostParams = Encoding.UTF8.GetBytes(submissionReq); // получение массива байтов из строки с параметрами. 
            submissionRequest.ContentLength = EncodedPostParams.Length;

            // Записываем данные в поток
            submissionRequest.GetRequestStream().Write(EncodedPostParams,
                                             0,
                                             EncodedPostParams.Length); // запись в поток запроса (массив байтов, хз, сколько запиливаем)

            submissionRequest.GetRequestStream().Close();

            // Получаем ответ
            HttpWebResponse submissionResponse = (HttpWebResponse)submissionRequest.GetResponse(); // получаем ответ

            // Получаем html-код страницы
            string submissionResult = new StreamReader(submissionResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd(); // считываем поток ответа

            //MessageBox.Show(submissionResult);
            if (!submissionResult.Contains("OK")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
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
            Song tmp = new Song{ Artist = tbArtist.Text, Track = tbTrack.Text, Album = tbAlbum.Text};
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
                lblSongCount.Text = lbList.Items.Count.ToString();
                
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (lbList.Items.Count == 0)
            {
                MessageBox.Show("Не добавлено ни одного трека!");
                return;
            }

            /*
            if (lbList.Items.Count > 50)
            {
                MessageBox.Show("Треков должно быть не более 50!");
                return;
            }
            */

            try
            {
                List<Song> tmp = new List<Song>();
                foreach (var T in lbList.Items)
                {
                    tmp.Add((Song)T);
                }

                int iterations = 1;
                if (tmp.Count > 50)
                {
                    if (tmp.Count % 50 > 0)
                        iterations = tmp.Count / 50 + 1;
                    else iterations = tmp.Count / 50;
                }

                for (int i = 0; i < iterations; i++)
                {
                    if (mLoginMode.Checked)
                    {
                        if (String.IsNullOrEmpty(tbLogin.Text) || String.IsNullOrEmpty(tbPassword.Text))
                        {
                            MessageBox.Show("Не заполнены нужные поля!");
                            return;
                        }
                        //List<Song> d = GetButch(50 * i, 50, tmp);
                        ScrobbleTracks(tbLogin.Text, tbPassword.Text, GetButch(50*i, 50, tmp));
                    }
                    else
                    {
                       // List<Song> d = GetButch(50 * i, 50, tmp);
                        ScrobbleTracks(GetButch(50 * i, 50, tmp));
                    }
                    if ((i+1) != iterations)
                    Thread.Sleep(1500); // ставим ожидание, чтобы дать серверу время на обработку
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

            if (mClear.Checked)
            {
                lbList.Items.Clear();
                lblSongCount.Text = lbList.Items.Count.ToString();
            }

            if (mLog.Checked)
            {
                SaveLog(true);
            }

            MessageBox.Show("Треки отправлены!");
        }

        void LoadConfig()
        {
                FileStream f = new FileStream(defFile, FileMode.Open);
                ApplicationSettings tmp = (ApplicationSettings)bf.Deserialize(f);
                AutoFill = tmp.AutoFill; // переброс ссылки, темп будет уничтожен, но ссылка на этот лист будет жить
                tbPassword.Text = tmp.Password;
                tbLogin.Text = tmp.Username;
                sessionKey = tmp.SessionKey;
                mLog.Checked = tmp.Log;
                mClear.Checked = tmp.Clear;
                mLoginMode.Checked = (tmp.Mode == 0) ? true : false;
                mAccessMode.Checked = !mLoginMode.Checked;
                tbArtist.DataSource = AutoFill;
                mSaveArtists.Checked = tmp.SaveArtists;
                mAutoClear.Checked = tmp.AutoClear;
                mAutoSave.Checked = tmp.AutoSave;
                f.Close();
        }

        public List<Song> GetButch(int start, int count, List<Song> t) 
        {
            if (count+start > t.Count)
            {
                return t.GetRange(start, t.Count - start);
            }
            else
            {
                return t.GetRange(start, count);
            }
        }

        bool CheckAuthorisation(string login, string password)
        {
            TimeSpan w = DateTime.Now - (new DateTime(1970, 1, 1, 0, 0, 0));
            TimeSpan t1 = new TimeSpan(3, 0, 0);
            w -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
            int t = (int)w.TotalSeconds;
            string client_id = "tst";
            string client_ver = "1.0";
            //string user = "v_decadence";
            string timestamp = t.ToString();

            string auth = MD5(MD5(password) + timestamp);//"";//токен авторизации который равен md5(md5(пароль) + timestamp)
            string req = "http://post.audioscrobbler.com/?hs=true&p=1.2.1&c=" + client_id + "&v=" + client_ver + "&u=" + login + "&t=" + timestamp + "&a=" + auth;// + pre); // создаём с адресом в параметрах.
            //HttpWebRequest fr = (HttpWebRequest)WebRequest.Create("http://www.google.com/");
            // пример получения исходного кода страницы через запрос
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(req);

            request.UserAgent = "Mozilla/5.0"; //
            request.Timeout = 6000; // для того, чтобы программа не зависала, если интернет-соединение отсутствует
            request.AllowAutoRedirect = true; // допустимо ли перенаправление
            // request.Referer = "http://ffdr.ru/"; // адрес с которого мы якобы пришли на запрашиваемую страницу
            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // получение ответа от сервераа

            string result = new StreamReader(response.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd(); // чтение потока (поток ответа, кодировка).чтение потока до конца.
            if (result.Contains("OK"))
                return true;
            else return false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            
        }


        private void tbTrack_Enter(object sender, EventArgs e)
        {
            tbTrack.SelectAll();
        }

        private void tbArtist_Enter(object sender, EventArgs e)
        {
            tbArtist.SelectAll();
        }

        public void AddOneLine(string s)
        {
            string artist = String.Empty;
            string track = String.Empty;
            string album = String.Empty;
            bool art = true;
            bool tr = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ' && s[i + 1] == '-' && s[i + 2] == ' ')
                {
                    if (art)
                    {
                        art = false;
                        tr = true;
                        i += 3;
                    }
                    else
                    {
                        tr = false;
                        i += 3;
                    }
                }

                if (art)
                {
                    artist += s[i];
                }
                else if (tr)
                {
                    track += s[i];
                }
                else
                {
                  //  if (!((i) >= s.Length))
                        album += s[i];
                }
            }
            lbList.Items.Add(new Song { Artist = artist, Track = track, Album = album });
            
        }

        private void btnOneLine_Click(object sender, EventArgs e)
        {
            if (tbOneLine.Text.Length > 4)
            AddOneLine(tbOneLine.Text);

            lblSongCount.Text = lbList.Items.Count.ToString();
            tbOneLine.Text = String.Empty;
        }

        private void DeleteMS_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedIndex > -1)
            {
                lbList.Items.RemoveAt(lbList.SelectedIndex);
                lblSongCount.Text = lbList.Items.Count.ToString();
            }
            else
            {
                MessageBox.Show("Выберите трек!", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ClearMS_Click(object sender, EventArgs e)
        {
            lbList.Items.Clear();
            lblSongCount.Text = "0";
        }

        private void ArtistMS_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedIndex > -1)
            {
                Song tmp = (Song)lbList.SelectedItem;
                Process.Start("http://last.fm/music/" + tmp.Artist);
            }

            else
            {
                MessageBox.Show("Выберите трек!", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SongMS_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedIndex > -1)
            {
                Song tmp = (Song)lbList.SelectedItem;
                Process.Start("http://last.fm/music/" + tmp.Artist + "/_/" + tmp.Track);
            }

            else
            {
                MessageBox.Show("Выберите трек!", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(defFile))
                {
                    LoadConfig();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        void SaveConfig()
        {
                FileStream g = new FileStream(defFile, FileMode.OpenOrCreate);
                ApplicationSettings tmp = new ApplicationSettings(tbLogin.Text, tbPassword.Text, sessionKey, ((mLoginMode.Checked) ? 0 : 1), mLog.Checked, mClear.Checked, mSaveArtists.Checked, mAutoSave.Checked, mAutoClear.Checked);
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

        private void mMode_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!mAccessMode.Checked)
            {
                gbAuthorisation.Enabled = true;
                mGiveAccess.Enabled = false;
            }
            else
            {
                mGiveAccess.Enabled = true;
                gbAuthorisation.Enabled = false;
            }
           // mAccessMode.Checked = !mLoginMode.Checked;
            
        }

        private void mLoginMode_Click(object sender, EventArgs e)
        {
            mAccessMode.Checked = mLoginMode.Checked;
            mLoginMode.Checked = !mLoginMode.Checked;
        }

        private void mAccessMode_Click(object sender, EventArgs e)
        {
            mLoginMode.Checked = mAccessMode.Checked;
            mAccessMode.Checked = !mAccessMode.Checked;
        }

        private void mDeleteData_Click(object sender, EventArgs e)
        {
            try
            {
                ClearConfig();
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
                    ApplicationSettings tmp = new ApplicationSettings(String.Empty, String.Empty, String.Empty, 0, false, false, false, false, false);
                    tmp.AutoFill.Clear();
                    bf.Serialize(g, tmp);
                    tbPassword.Text = String.Empty;
                    tbLogin.Text = String.Empty;
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
                if (MessageBox.Show("Уверены?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {

                    if (mAutoClear.Checked)
                        ClearConfig();
                    else if (mAutoSave.Checked)
                        SaveConfig();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void mCheckLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbLogin.Text) || String.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Не заполнены нужные поля!");
                return;
            }
            else
            {
                if (CheckAuthorisation(tbLogin.Text, tbPassword.Text))
                    MessageBox.Show("Данные введены верно!");
                else MessageBox.Show("Данные введены неверно!");
            }
        }

        private void mProfile_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLogin.Text))
            Process.Start("http://lastfm.ru/user/" + tbLogin.Text);
            else MessageBox.Show("Введите имя пользователя!");
        }

        public void GiveAccess()
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.gettoken&api_key=" + ApiKey); // получение токена
            HttpWebResponse tokenResponse = (HttpWebResponse)tokenRequest.GetResponse();
            string tokenResult = new StreamReader(tokenResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd();
            // извлечение токена
            string token = String.Empty;
            for (int i = tokenResult.IndexOf("<token>") + 7; i < tokenResult.IndexOf("</token"); i++)
            {
                token += tokenResult[i];
            }
            //token = token.Remove(0, 7); // удаляем первый тег.

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
                string sessK = String.Empty;

                // извлечение сессии
                for (int i = sessionResult.IndexOf("<key>") + 5; i < sessionResult.IndexOf("</key>"); i++)
                {
                    sessK += sessionResult[i];
                }
                sessionKey = sessK;
                // Settings.Default.sessionKey = "fsddf";
            }
            else
            {
                throw new Exception("Вы должны предоставить доступ приложению!");
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
            if (lbList.SelectedIndex > -1)
            {
                if (!String.IsNullOrEmpty(tbLogin.Text))
                {
                    Song tmp = (Song)lbList.SelectedItem;
                    Process.Start("http://last.fm/user/" + tbLogin.Text+"/library/music/" + tmp.Artist);
                }
                else MessageBox.Show("Введите имя пользователя!", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                MessageBox.Show("Выберите трек!", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mAbout_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Автономный Last.fm скробблер, версия 1.2.2.0 (26 октября 2011 года) © 2011, Исадов Виктор. Все права защищены." + Environment.NewLine + "Открыть страницу программы?", "О программе", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                //Process.Start("http://vkontakte/victor_decadence");
                Process.Start("http://vkontakte.ru/note4223988_10790267");
            }
        }

        private void pbLoved_Click(object sender, EventArgs e)
        {
            try
            {

            DialogResult d = MessageBox.Show("Добавить текущий трек в любимые? Для этого нужно дать приложению доступ к вашему профилю, а также ввести логин/пароль. Трек также будет заскробблен.", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
            
            if (d == DialogResult.Yes)
              {
                if (String.IsNullOrEmpty(tbArtist.Text) || String.IsNullOrEmpty(tbTrack.Text) || String.IsNullOrEmpty(tbPassword.Text) || String.IsNullOrEmpty(tbLogin.Text))
                {
                    MessageBox.Show("Не введены необходимые данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sessK = GetSessionKey();

                if (String.IsNullOrEmpty(sessK))
                {
                    DialogResult f = MessageBox.Show("Вы должны предоставить приложению доступ к своему профилю. Сделать это сейчас?", "Ошибка", MessageBoxButtons.YesNo);
                    if (f == DialogResult.Yes)
                    {
                        mGiveAccess_Click(null, EventArgs.Empty);
                    }
                    else
                    {
                        throw new Exception("Вы должны предоставить приложению доступ к своему профилю! Или выберите другой режим отправки треков.");
                    }
                }
                
                TrackLoved(tbArtist.Text, tbTrack.Text);
                MessageBox.Show("Трек заскробблен и добавлен в любимые!");
            }
            else
                return;

            }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
        }

        void TrackLoved(string artist, string track)
        {

            // новый апи метод track.love должен следовать за старым скробблингом (самбишин) песни с рейтингом L.
            List<Song> s = new List<Song>();
            s.Add(new Song { Artist = artist, Track = track });
            ScrobbleTracks(tbLogin.Text, tbPassword.Text, s); // делаем наш запрос с одной песней
            
            Thread.Sleep(2000); // ждём пару секунд, чтобы убедиться, что сервер ласта успел обработать предыдущий запрос.

            string sessK = GetSessionKey();

            string signature = String.Empty;
            string parametres = String.Empty;

            signature += "api_key" + ApiKey + "artist" + artist + "methodtrack.lovesk" + sessK + "track" + track + mySecret;


            // parametres += "method=track.love&track=" + HttpUtility.UrlEncode(track) + "artist=" + HttpUtility.UrlEncode(artist) + "&api_key=" + ApiKey + "&sk=" + sessK + "&api_sig=" + MD5(signature);
            parametres += "method=track.love&artist=" + HttpUtility.UrlEncode(artist) + "&track=" + HttpUtility.UrlEncode(track) + "&api_key=" + ApiKey + "&sk=" + sessK + "&api_sig=" + MD5(signature); 
            //parametres = "dgfgdfg";
            HttpWebRequest lovedRequest = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/2.0/");
            lovedRequest.ServicePoint.Expect100Continue = false; // важная строка! из-за её отсутствия сервер ласта рвал соединение

            // Настраиваем параметры запроса
            lovedRequest.UserAgent = "Mozilla/5.0";
            lovedRequest.Method = "POST"; // Указываем метод отправки данных скрипту, в случае с Post обязательно
            lovedRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные
            lovedRequest.Timeout = 5000; // 

            byte[] param = Encoding.UTF8.GetBytes(parametres);
            lovedRequest.ContentLength = param.Length;

            lovedRequest.GetRequestStream().Write(param, 0, param.Length);
            lovedRequest.GetRequestStream().Close();

            HttpWebResponse lovedResponse = (HttpWebResponse)lovedRequest.GetResponse();
            string lovedResult = new StreamReader(lovedResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd();

            if (!lovedResult.Contains("status=\"ok\"")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
                throw new Exception("Трек не добавлен в любимые! Причина - " + lovedResult);
           // MessageBox.Show(lovedResult);
        }


        private void mGetTracks_Click(object sender, EventArgs e)
        {
            FrmExport frmEx = new FrmExport(tbLogin.Text);
            frmEx.Show();
            //string j = GetTracks("v_decadence", null);
           // int g = 0;
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
            tbAlbum.Visible = lblAlbum.Visible = mWithAlbum.Checked;
        }


    }
}


#region Мясо
/*
void ScrobbleOneTrack(string login, string password, string artist, string track, int time)
        {
            
            string client_id = "tst"; // для тестовых клиентов используется этот идентификатор
            string client_ver = "1.0";
            string auth = MD5(MD5(password) + time);//токен авторизации, который равен md5(md5(пароль) + timestamp)
            string handshakeReq = "http://post.audioscrobbler.com/?hs=true&p=1.2.1&c=" + client_id + "&v= " + client_ver + "&u=" + login + "&t=" + time + "&a=" + auth;// + pre); // создаём с адресом в параметрах.

            HttpWebRequest handshakerequest = (HttpWebRequest)WebRequest.Create(handshakeReq);
            handshakerequest.UserAgent = "Mozilla/5.0"; // 
            handshakerequest.AllowAutoRedirect = true; // допустимо ли перенаправление
            HttpWebResponse handshakeResponse = (HttpWebResponse)handshakerequest.GetResponse(); // получение ответа от сервераа
            string handshakeResult = new StreamReader(handshakeResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd(); // чтение потока (поток ответа, кодировка).чтение потока до конца.
            ///////////////////////////////////

            // Формируем строку с параметрами
            // выбираем вторую строку с айди сессии
            string sessID = String.Empty;
            int d = 0;
            for (int i = 0; i < handshakeResult.Length; i++)
            {
                if (handshakeResult[i] == '\n')
                {
                    d++;
                    continue;
                }
                if (d > 1) break;
                if (d == 1) sessID += handshakeResult[i];
            }

            string submissionReq = "s=" + HttpUtility.UrlEncode(sessID) + "&a[0]=" + HttpUtility.UrlEncode(artist) + "&t[0]=" + HttpUtility.UrlEncode(track) + "&i[0]=" + HttpUtility.UrlEncode(time.ToString()) + "&o[0]=P&r[0]=L&l[0]=300&b[0]=" + String.Empty + "&n[0]=1&m[0]=" + String.Empty;//&a[1]=" + HttpUtility.UrlEncode("Uknown") + "&t[1]=" + HttpUtility.UrlEncode("1213") + "&i[1]=" + HttpUtility.UrlEncode("455465") + "&o[1]=P&r[1]=L&l[1]=120&b[1]=\"\"&n[1]=\"\"&m[1]=\"\"";

            HttpWebRequest submissionRequest = (HttpWebRequest)WebRequest.Create("http://post2.audioscrobbler.com:80/protocol_1.2"); // адрес запроса (без параметров)

            // Настраиваем параметры запроса
            submissionRequest.UserAgent = "Mozilla/5.0";
            submissionRequest.Method = "POST"; // Указываем метод отправки данных скрипту, в случае с Post обязательно

            
            submissionRequest.AllowAutoRedirect = true;
            // request.Referer = mainSiteUrl;
            // request.CookieContainer = cookieCont;

            // Указываем тип отправляемых данных
            submissionRequest.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена). В случае с Post обязательна, видимо из-за её отсутствия неправильно кодировались данные

            // Преобразуем данные к соответствующую кодировку
            byte[] EncodedPostParams = Encoding.UTF8.GetBytes(submissionReq); // получение массива байтов из строки с параметрами
            submissionRequest.ContentLength = EncodedPostParams.Length;

            // Записываем данные в поток
            submissionRequest.GetRequestStream().Write(EncodedPostParams,
                                             0,
                                             EncodedPostParams.Length); // запись в поток запроса (массив байтов, хз, сколько запиливаем)

            submissionRequest.GetRequestStream().Close();

            // Получаем ответ
            HttpWebResponse submissionResponse = (HttpWebResponse)submissionRequest.GetResponse(); // получаем ответ

            // Получаем html-код страницы
            string submissionResult = new StreamReader(submissionResponse.GetResponseStream(),
                                           Encoding.UTF8).ReadToEnd(); // считываем поток ответа
           
            if (!submissionResult.Contains("OK")) // проверяет, содержит ли строка эту подстроку, с учётом регистра
                throw new Exception("Треки не отправлены!");
        }
 */

/*
 try
                {
                    pBMain.Visible = true;
                    double i = 0;
                    // установка сеанса работы с сервером
                    TimeSpan rtime = DateTime.Now - (new DateTime(1970, 1, 1, 0, 0, 0));
                    TimeSpan t1 = new TimeSpan(3, 0, 0);
                    rtime -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
                    int timestamp = (int)rtime.TotalSeconds; // общее количество секунд
                    foreach (object t in lbList.Items)
                    {
                        Song tmp = (Song)t;
                        ScrobbleOneTrack(tbLogin.Text, tbPassword.Text, tmp.Artist, tmp.Track, timestamp);
                        timestamp -= 300;
                        i++;
                        pBMain.Value = (int)((i / lbList.Items.Count) * 100);
                        Thread.Sleep(500);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pBMain.Visible = false;
                    pBMain.Value = 0;
                    return;
                }
                // если метод не завершить принудительно, всё, что после catch - сработает
 */

//        private void btnTest_Click(object sender, EventArgs e)
//        {
//            TimeSpan w = DateTime.Now - (new DateTime(1970, 1, 1, 0, 0, 0));
//            TimeSpan t1 = new TimeSpan(3,0,0);
//            w -= t1;
//            int t = (int)w.TotalSeconds;
//            string client_id = "tst";
//            string client_ver = "1.0";
//            string user = "v_decadence";
//            string timestamp = t.ToString();

//            string auth = MD5(MD5("fuckINbitch") + timestamp);//"";//токен авторизации который равен md5(md5(пароль) + timestamp)
//            string req = "http://post.audioscrobbler.com/?hs=true&p=1.2.1&c=" + client_id + "&v= " + client_ver + "&u=" + user + "&t=" + timestamp + "&a=" + auth;// + pre); // создаём с адресом в параметрах.
//            //HttpWebRequest fr = (HttpWebRequest)WebRequest.Create("http://www.google.com/");
//            // пример получения исходного кода страницы через запрос
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(req);


//            request.UserAgent = "Mozilla/5.0"; // 
//            request.AllowAutoRedirect = true; // допустимо ли перенаправление
//            // request.Referer = "http://ffdr.ru/"; // адрес с которого мы якобы пришли на запрашиваемую страницу
//            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // получение ответа от сервераа

//            string html = new StreamReader(response.GetResponseStream(),
//                                           Encoding.UTF8).ReadToEnd(); // чтение потока (поток ответа, кодировка).чтение потока до конца.
//            MessageBox.Show(html);


//            ///////////////////////////////////

//            // Формируем строку с параметрами

//            // выбираем вторую строку с айди сессии
//            string tmp = String.Empty;
//            int d = 0;
//            for (int i = 0; i < html.Length; i++)
//            {
//                if (html[i] == '\n')
//                {
//                    d++;
//                    continue;

//                }

//                if (d > 1) break;
//                if (d == 1) tmp += html[i];
//            }
//            string sessId = tmp;
//            string artist = "yuy";
//            string track = "s12";

//            //  string req2 = "";
//            //  for (int i = 0; i < 2; i++)
//            // {
//            //    req


//            // }
//            // string req2 = "s=" + HttpUtility.UrlEncode(sessId) + "&a[0]=" + HttpUtility.UrlEncode(artist) + "&t[0]=" + HttpUtility.UrlEncode(track) + "&i[0]=" + HttpUtility.UrlEncode(timestamp);
//            string req2 = "s=" + HttpUtility.UrlEncode(sessId) + "&a[0]=" + HttpUtility.UrlEncode(artist) + "&t[0]=" + HttpUtility.UrlEncode(track) + "&i[0]=" + HttpUtility.UrlEncode(timestamp) + "&o[0]=P&r[0]=L&l[0]=120&b[0]=\"\"&n[0]=\"\"&m[0]=\"\"&a[1]=" + HttpUtility.UrlEncode("Uknown") + "&t[1]=" + HttpUtility.UrlEncode("1213") + "&i[1]=" + HttpUtility.UrlEncode("455465") + "&o[1]=P&r[1]=L&l[1]=120&b[1]=\"\"&n[1]=\"\"&m[1]=\"\"";

//            //  string req2 = "s=" + HttpUtility.UrlEncode(sessId) + "&a[0]=" + HttpUtility.UrlEncode(artist) + "&t[0]=" + HttpUtility.UrlEncode(track) + "&i[0]=" + HttpUtility.UrlEncode(timestamp);
//            // string req2 = "s=" + sessId + "&a[0]=" + artist + "&a[1]=df&t[0]=" + track + "&t[1]=sdfsd&i[0]=" + timestamp + "&i[1]=" + timestamp + "&o[0]=P&o[1]=P&r[0]=L&r[1]=L&l[0]=120&l[1]&b[0]=''&b[1]=''&n[0]=''&n[1]=''&m[0]=''&m[1]=''";



//            /*
//             r[0]=<rating>
//A single character denoting the rating of the track. Empty if not applicable. 
//L
//Love (on any mode if the user has manually loved the track). This implies a listen.
//B
//Ban (only if source=L). This implies a skip, and the client should skip to the next track when a ban happens.
//S
//Skip (only if source=L)

//Note: Currently a Last.fm web service must also be called to set love (track.love) or ban (track.ban) status. We anticipate that the next version of the scrobble protocol will no longer perform love and ban and this will instead be handled by the web services only.
//l[0]=<secs>
//The length of the track in seconds. Required when the source is P, optional otherwise.
//b[0]=<album>
//The album title, or an empty string if not known.
//n[0]=<tracknumber>
//The position of the track on the album, or an empty string if not known.
//m[0]=<mb-trackid>
//The MusicBrainz Track ID, or an empty string if not known.
//             */
//            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("http://post2.audioscrobbler.com:80/protocol_1.2"); // адрес запроса (без параметров)

//            // Настраиваем параметры запроса
//            request1.UserAgent = "Mozilla/5.0";
//            request1.Method = "POST"; // в случае с Post обязательно

//            // Указываем метод отправки данных скрипту
//            request1.AllowAutoRedirect = true;
//            // request.Referer = mainSiteUrl;
//            // request.CookieContainer = cookieCont;

//            // Указываем тип отправляемых данных
//            request1.ContentType = "application/x-www-form-urlencoded"; // основная строчка из-за которой не работало! (точнее, из-за того, что она была закомменчена)

//            // Преобразуем данные к соответствующую кодировку
//            byte[] EncodedPostParams = Encoding.ASCII.GetBytes(req2); // получение массива байтов из строки с параметрами
//            request1.ContentLength = EncodedPostParams.Length;

//            // Записываем данные в поток
//            request1.GetRequestStream().Write(EncodedPostParams,
//                                             0,
//                                             EncodedPostParams.Length); // запись в поток запроса (массив байтов, хз, сколько запиливаем)

//            request1.GetRequestStream().Close();

//            // Получаем ответ
//            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse(); // получаем ответ

//            // Получаем html-код страницы
//            string html1 = new StreamReader(response1.GetResponseStream(),
//                                           Encoding.UTF8).ReadToEnd(); // считываем поток ответа
//            MessageBox.Show(html1);
//        }



#endregion



// добавить в референс System.Web, для работы с HttpUtility
// как впихнуть пустую строку? (видимо, String.Empty всё-таки работает. Тогда она принимает вид &s=&r=4... Пробелы между недопустимы (только между словами в значении параметра))

// при добавлении новых песен он берёт альбом, продолжительности и т.д. с твоего запроса, то есть при скробблинге уже существующих можно не париться (главное, исполнитель и название трека). Числовые поля лучше всё-таки заполнять хоть чем-то, ибо сервер может не воспринять песню. Но лучше 1 раз песню добавлять с правильной продолжительностью из другого места
// так что лучше отсюда добавлять только те песни, которые уже есть в библиотеке, чтобы не создавать кривые второстепенные данные (альбом, продолжительность и т.д. :))

//вычитание даты:
/*
TimeSpan t1 = new TimeSpan(3, 0, 0);
w -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
*/


//lbList.Items.Add(new Song { Artist = artist, Track = track });  - так можно заполнить только поля!

// Uri Encode for &
// приостановка работы программы с помощью диалог резалта и месаджбокса
// рабочая строчка!
// string submissionReq = "s=" + sessID + "&a[0]=" + artist + "&a[1]=Disturbed&t[0]=" + track + "&t[1]=Session&i[0]=" + timestamp + "&i[1]=" + (timestamp+300) + "&o[0]=P&o[1]=P&r[0]=L&r[1]=L&l[0]=300&l[1]=300&b[0]=" + String.Empty + "&b[1]=" + String.Empty + "&n[0]=1&n[1]=1&m[0]=" + String.Empty + "&m[1]=" + String.Empty;
// при мулитьотправке треков параметры должны идти не a[0]...m[0]&a[1], a a[0]&a[1]...m[0]&m[1]. Желательно вставлять разное время прослушивания. Кодировать необязательно, вместо пустых значений ставим String.Empty.

/*
string ziga = MD5("api_key"+ApiKey+"artist[0]prodigymethodtrack.scrobblesk"+sessK+"timestamp[0]"+timestamp+"track[0]smack"+mySecret);
                string sf = "method=track.scrobble&sk=" + sessK + "&api_key=" + ApiKey + "&artist[0]=prodigy&track[0]=smack&timestamp[0]=" + timestamp + "&api_sig=" + ziga;
                string temp3 = "method=track.scrobble&track[0]=Rocket&artist[0]=Defueppard&timestamp[0]=" + (timestamp - 300).ToString() + "&track[1]=Women&artist[1]=DefLeppard&timestamp[1]=" + timestamp.ToString() + "&api_key=" + ApiKey + "&api_sig=" + ziga + "&sk=" + sessK;
 рабочие строки для нового АПИ
 
 * api_keyea13f7fe78f7ef62bbbc5c43dcd6dfcbartist[0]Example Artistmethodtrack.scrobbleskf448e2ecfb287a466162b24efdf588ectimestamp[0]1298908503track[0]Example Trackaeaa72cd88242eb7f1437ed8b9937e16"
 
 method=track.scrobble&sk=f448e2ecfb287a466162b24efdf588ec&api_key=ea13f7fe78f7ef62bbbc5c43dcd6dfcb&artist[0]=Example Artist&timestamp[0]=1298908803&track[0]=Example Track&api_sig=141694047efdb18d29916b6bc4bf0b39
 
 */

// сигнатура делается отдельно для каждого(!) апи-метода и в ней параметры располагаются имязначение в альфа-порядке

// не совпадение сигнатуры и отправки из-за ёбаного таймстампа!

// написать про мессаджбокс, блокирование им и диалог резалт от него.

/*
 int[] t = { 0, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 1, 20, 2, 3, 4, 5, 6, 7, 8, 9}; // правильный порядок при мультиотправке
*/
//e.Cancel = true;

/*
tbArtist.DataSource = null;
tbArtist.DataSource = AutoFill;
tbArtist.PerformLayout(); комбобокс только так обновляет свой датасорс сразу!*/