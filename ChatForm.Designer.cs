namespace TcpChat
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtNick;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCreateServer;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblNick;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblAllMessages;
        private System.Windows.Forms.Label lblMessage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtNick = new System.Windows.Forms.TextBox();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCreateServer = new System.Windows.Forms.Button();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblNick = new System.Windows.Forms.Label();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblAllMessages = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNick
            // 
            this.lblNick.AutoSize = true;
            this.lblNick.Location = new System.Drawing.Point(12, 15);
            this.lblNick.Name = "lblNick";
            this.lblNick.Size = new System.Drawing.Size(29, 13);
            this.lblNick.TabIndex = 0;
            this.lblNick.Text = "Нік:";
            // 
            // txtNick
            // 
            this.txtNick.Location = new System.Drawing.Point(60, 12);
            this.txtNick.Name = "txtNick";
            this.txtNick.Size = new System.Drawing.Size(140, 20);
            this.txtNick.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(280, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(140, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Приєднання до сервера";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 45);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(93, 13);
            this.lblIp.TabIndex = 3;
            this.lblIp.Text = "IP-адрес сервера:";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(120, 42);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(140, 20);
            this.txtServerIp.TabIndex = 4;
            this.txtServerIp.Text = "127.0.0.1";
            // 
            // btnCreateServer
            // 
            this.btnCreateServer.Location = new System.Drawing.Point(280, 40);
            this.btnCreateServer.Name = "btnCreateServer";
            this.btnCreateServer.Size = new System.Drawing.Size(140, 23);
            this.btnCreateServer.TabIndex = 5;
            this.btnCreateServer.Text = "Створити сервер";
            this.btnCreateServer.UseVisualStyleBackColor = true;
            this.btnCreateServer.Click += new System.EventHandler(this.btnCreateServer_Click);
            // 
            // lblAllMessages
            // 
            this.lblAllMessages.AutoSize = true;
            this.lblAllMessages.Location = new System.Drawing.Point(12, 75);
            this.lblAllMessages.Name = "lblAllMessages";
            this.lblAllMessages.Size = new System.Drawing.Size(83, 13);
            this.lblAllMessages.TabIndex = 6;
            this.lblAllMessages.Text = "Всі повідомлення";
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(12, 95);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(408, 180);
            this.txtChat.TabIndex = 7;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 285);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(67, 13);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "Повідомлення";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(90, 282);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(210, 20);
            this.txtMessage.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(310, 280);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(110, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Послати повідомлення";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(310, 310);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Вихід";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ChatForm
            // 
            this.ClientSize = new System.Drawing.Size(432, 345);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.lblAllMessages);
            this.Controls.Add(this.btnCreateServer);
            this.Controls.Add(this.txtServerIp);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtNick);
            this.Controls.Add(this.lblNick);
            this.Name = "ChatForm";
            this.Text = "Чат";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
