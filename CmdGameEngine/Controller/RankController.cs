using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CmdGameEngine.Model;
using System.Windows.Forms.VisualStyles;
using CmdGameEngine.GameEngine.Helper;

namespace CmdGameEngine.Controller
{
    public class RankController
    {
        #region 单例
        static RankController ins = null;

        RankController()
        {
            init();
        }

        public static RankController Ins
        {
            get
            {
                if (ins == null)
                {
                    ins = new RankController();
                }
                return ins;
            }
        }
        #endregion

        public Mode1Rank m1r = new Mode1Rank();

        public Mode3Rank m3r = new Mode3Rank();

        public void init()
        {
            if (!Directory.Exists(@"data"))
            {
                Directory.CreateDirectory(@"data");
            }
            FileStream fs = new FileStream(@"data/mode1RankInfo.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string m1Str = sr.ReadToEnd();

            if (m1Str.Trim().Length == 0)
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(JsonHelper.ToJson(m1r));
                sw.Close();
            }
            else
            {
                m1r = JsonHelper.ToObj<Mode1Rank>(m1Str);
                m1r.datas.Sort();
            }
            sr.Close();


            fs = new FileStream(@"data/mode3RankInfo.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sr = new StreamReader(fs);
            string m3Str = sr.ReadToEnd();

            if (m3Str.Trim().Length == 0)
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(JsonHelper.ToJson(m3r));
                sw.Close();
            }
            else
            {
                m3r = JsonHelper.ToObj<Mode3Rank>(m3Str);
                m3r.datas.Sort();
            }
            sr.Close();


            //StreamWriter sw = new StreamWriter(fs); // 创建写入baidu流
            //sw.WriteLine("Hello World"); // 写入Hello World
            //sw.Close(); //关闭文件
        }

        public void AddMode1Rank(string name, int data)
        {
            if (name.Trim().Length == 0) return;
            RankItem ri = new RankItem()
            {
                name = name,
                value = data,
            };

            bool isHave = false;

            foreach (RankItem item in m1r.datas)
            {
                if (item.name == name)
                {
                    if (item.value < data)
                    {
                        m1r.datas[m1r.datas.IndexOf(item)].value = data;
                    }
                    isHave = true;
                    break;
                }
            }

            if (!isHave)
            {
                m1r.datas.Add(ri);
            }

            m1r.datas.Sort();

            FileStream fs = new FileStream(@"data/mode1RankInfo.json", FileMode.Create, FileAccess.ReadWrite);

            StreamWriter sw = new StreamWriter(fs);
            sw.Write(JsonHelper.ToJson(m1r));

            sw.Close();
        }

        public void AddMode3Rank(string name, int data)
        {
            if (name.Trim().Length == 0) return;
            RankItem ri = new RankItem()
            {
                name = name,
                value = data,
            };

            bool isHave = false;

            foreach (RankItem item in m3r.datas)
            {
                if (item.name == name)
                {
                    if (item.value < data)
                    {
                        m3r.datas[m3r.datas.IndexOf(item)].value = data;
                    }
                    isHave = true;
                    break;
                }
            }

            if (!isHave)
            {
                m3r.datas.Add(ri);
            }

            m3r.datas.Sort();

            FileStream fs = new FileStream(@"data/mode3RankInfo.json", FileMode.Create, FileAccess.ReadWrite);

            StreamWriter sw = new StreamWriter(fs);
            sw.Write(JsonHelper.ToJson(m3r));

            sw.Close();
        }


    }
}
