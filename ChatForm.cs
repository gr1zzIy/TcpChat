using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TcpChat
{
    public partial class ChatForm : Form
    {
        private TcpListener _server;
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _listenThread, _receiveThread;
        private readonly List<TcpClient> _connectedClients = new List<TcpClient>();
        private readonly Dictionary<TcpClient, string> _clientNicks = new Dictionary<TcpClient, string>();
        private bool _isExiting;
        
        public ChatForm()
        {
            InitializeComponent();
        }

        private void btnCreateServer_Click(object sender, EventArgs e)
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, 8888);
                _server.Start();
                AppendMessage("Сервер запущено...");

                _listenThread = new Thread(Start)
                {
                    IsBackground = true
                };
                _listenThread.Start();

                // Під'єднати локально як клієнта теж
                ConnectToServer("127.0.0.1");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка запуску сервера: " + ex.Message);
            }
        }

        private void Start()
        {
            while (true)
            {
                TcpClient newClient = _server.AcceptTcpClient();
                AppendMessage("Клієнт підключився");
                Thread t = new Thread(HandleClient);
                t.Start(newClient);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            string clientNick = "Користувач"; // тимчасове ім'я
            bool normalExit = false; // чи нормальний вихід

            lock (_connectedClients) 
            {
                _connectedClients.Add(tcpClient);
                _clientNicks[tcpClient] = clientNick;
            }

            NetworkStream ns = tcpClient.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = ns.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    
                    // Перевіряємо чи це повідомлення про вихід
                    if (msg.Contains(" покинув(ла) чат"))
                    {
                        normalExit = true;
                        
                        // Отримуємо нік з повідомлення про вихід
                        int separatorIndex = msg.IndexOf(" покинув(ла) чат", StringComparison.Ordinal);
                        if (separatorIndex > 0)
                        {
                            BroadcastMessage(msg);
                            AppendMessage(msg);
                            
                            // Видаляємо клієнта
                            lock (_connectedClients)
                            {
                                _clientNicks.Remove(tcpClient);
                                _connectedClients.Remove(tcpClient);
                            }
                            
                            tcpClient.Close();
                            return; // Виходимо з потоку
                        }
                    }
                    
                    // Отримуємо нік з повідомлення (формат: "нік: повідомлення")
                    if (msg.Contains(": "))
                    {
                        int separatorIndex = msg.IndexOf(": ", StringComparison.Ordinal);
                        string nick = msg.Substring(0, separatorIndex);
                        
                        // Оновлюємо нік користувача
                        lock (_connectedClients)
                        {
                            _clientNicks[tcpClient] = nick;
                        }
                        
                        BroadcastMessage(msg);
                    }
                    else if (msg.Contains(" приєднався до чату"))
                    {
                        // Оновлюємо нік для повідомлення про підключення
                        int separatorIndex = msg.IndexOf(" приєднався до чату", StringComparison.Ordinal);
                        if (separatorIndex > 0)
                        {
                            string nick = msg.Substring(0, separatorIndex);
                            lock (_connectedClients)
                            {
                                _clientNicks[tcpClient] = nick;
                            }
                        }
                        BroadcastMessage(msg);
                    }
                    else
                    {
                        BroadcastMessage(msg);
                    }
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                // Відправляємо повідомлення про вихід тільки якщо це не нормальний вихід
                if (!normalExit && !_isExiting)
                {
                    string nick;
                    lock (_connectedClients)
                    {
                        if (_clientNicks.TryGetValue(tcpClient, out nick))
                        {
                            _clientNicks.Remove(tcpClient);
                        }
                        _connectedClients.Remove(tcpClient);
                    }
                    
                    // Відправляємо повідомлення про вихід
                    if (!string.IsNullOrEmpty(nick))
                    {
                        string leaveMessage = $"{nick} покинув(ла) чат";
                        BroadcastMessage(leaveMessage);
                        AppendMessage(leaveMessage);
                    }
                }
                
                try
                {
                    if (tcpClient.Connected)
                        tcpClient.Close();
                }
                catch { /* ignore */ }
            }
        }

        // Трансляція всім клієнтам
        private void BroadcastMessage(string msg)
        {
            lock (_connectedClients)
            {
                // Створюємо копію списку для безпечної ітерації
                var clientsToRemove = new List<TcpClient>();
                
                foreach (var cl in _connectedClients)
                {
                    try
                    {
                        if (cl.Connected)
                        {
                            NetworkStream ns = cl.GetStream();
                            byte[] data = Encoding.UTF8.GetBytes(msg);
                            ns.Write(data, 0, data.Length);
                        }
                        else
                        {
                            clientsToRemove.Add(cl);
                        }
                    }
                    catch
                    {
                        clientsToRemove.Add(cl);
                    }
                }
                
                // Видаляємо від'єднаних клієнтів
                foreach (var cl in clientsToRemove)
                {
                    _connectedClients.Remove(cl);
                    _clientNicks.Remove(cl);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectToServer(txtServerIp.Text);
        }

        private void ConnectToServer(string ip)
        {
            try
            {
                _client = new TcpClient(ip, 8888);
                _stream = _client.GetStream();
                AppendMessage("Підключено до сервера");

                // Відправляємо повідомлення про підключення
                string nick = txtNick.Text.Trim();
                if (string.IsNullOrEmpty(nick)) nick = "Користувач";
                string joinMessage = $"{nick} приєднався(лась) до чату";
                byte[] joinData = Encoding.UTF8.GetBytes(joinMessage);
                _stream.Write(joinData, 0, joinData.Length);

                _receiveThread = new Thread(() =>
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        try
                        {
                            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0) break;

                            string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            AppendMessage(msg);
                        }
                        catch { break; }
                    }
                    
                    // Повідомлення про відключення (якщо вийшли не по своїй волі)
                    if (!_isExiting)
                    {
                        AppendMessage("Відключено від сервера");
                    }
                })
                {
                    IsBackground = true
                };
                _receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення: " + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_stream != null && _client != null && _client.Connected)
            {
                string nick = txtNick.Text.Trim();
                string text = txtMessage.Text.Trim();
                if (string.IsNullOrEmpty(nick) || string.IsNullOrEmpty(text)) return;

                string fullMsg = nick + ": " + text;
                byte[] data = Encoding.UTF8.GetBytes(fullMsg);
                _stream.Write(data, 0, data.Length);
                txtMessage.Clear();
            }
        }

        // Додайте метод для коректного закриття з'єднання
        private void DisconnectFromServer()
        {
            try
            {
                _isExiting = true;
                
                if (_client != null && _client.Connected)
                {
                    // Відправляємо повідомлення про вихід
                    string nick = txtNick.Text.Trim();
                    if (string.IsNullOrEmpty(nick)) nick = "Користувач";
                    string leaveMessage = $"{nick} покинув(ла) чат";
                    byte[] data = Encoding.UTF8.GetBytes(leaveMessage);
                    _stream.Write(data, 0, data.Length);
                    
                    Thread.Sleep(100); // Даємо час на відправку
                    
                    _client.Close();
                }
            }
            catch { /* ignore */ }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DisconnectFromServer();
            Application.Exit();
        }

        // Обробник закриття форми
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DisconnectFromServer();
            base.OnFormClosing(e);
        }

        private void AppendMessage(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => txtChat.AppendText(text + Environment.NewLine)));
            }
            else
            {
                txtChat.AppendText(text + Environment.NewLine);
            }
        }
    }
}