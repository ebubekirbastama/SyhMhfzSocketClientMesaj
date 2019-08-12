using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BekraMesajlasmaSocketClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Thread th;
        private void buttonX1_Click(object sender, EventArgs e)
        {
            mesajsend();
        }
        private void mesajsend()
        {
            soket.Send(Encoding.UTF8.GetBytes(richTextBox1.Text));
        }
        private void bağlantıYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (soket.Connected != true)
                {
                     soket.Connect(new IPEndPoint(IPAddress.Parse(textBoxX1.Text), int.Parse(textBoxX2.Text)));
                     MessageBox.Show("Başarıyla bağlanıldı.!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string[] dizi = { "Process Listing"};
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < dizi.Length; i++)
            {
                comboBoxEx1.Items.Add(dizi[i].ToString());
            } 
        }
    }
}
