using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomTextGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string m_ChoSungTbl = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";

        private static string m_JungSungTbl = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";

        private static string m_JongSungTbl = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";

        private static ushort m_UniCodeHangulBase = 0xAC00;

        private static ushort m_UniCodeHangulLast = 0xD79F;

        private string MergeChar(string ch,string j, string jon)
        {
            int ch_pos, j_pos, jon_pos;
            int nUnicode;
            ch_pos = m_ChoSungTbl.IndexOf(ch);
            j_pos = m_JungSungTbl.IndexOf(j);
            jon_pos = m_JongSungTbl.IndexOf(jon);
            nUnicode = m_UniCodeHangulBase + (ch_pos * 21 + j_pos) * 28 + jon_pos;
            char temp = Convert.ToChar(nUnicode);
            return temp.ToString();
        }
        private string MergeChar(int a, int b, int c)
        {
            int nUnicode;
            nUnicode = m_UniCodeHangulBase + (a * 21 + b) * 28 + c;
            char temp = Convert.ToChar(nUnicode);
            return temp.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = Convert.ToInt32(textBox2.Text);
            string msg = "";
            Random r = new Random();
            if(!checkBox2.Checked)//한글 생성 허용 안되면
            {
                if (checkBox1.Checked)
                {
                    //숫자 온리
                    for (int i = 0; i < length; i++)
                    {
                        int a = r.Next(0, 10);
                        msg += a.ToString();
                    }
                }
                else
                {
                    //안숫자 허용
                    for (int i = 0; i < length; i++)
                    {
                        int a = r.Next(33, 127);
                        msg += Convert.ToChar(a);
                    }
                }
            }
            else
            {
                for(int i = 0; i < length; i++)
                {
                    int a = r.Next(0, m_ChoSungTbl.Length);
                    int b = r.Next(0, m_JungSungTbl.Length);
                    int c = r.Next(0, m_JongSungTbl.Length);
                    msg+=MergeChar(a, b, c);
                }
            }
            msg += " kimdoeCAT";
            textBox1.Text = msg;
            Clipboard.SetText(msg);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
