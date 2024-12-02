using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Var58_Task5
{
    internal class Program
    {
        static void Main()
        {
            //  путь к файлу с сырым текстом
            const string raw_file_path = "..\\..\\Text.txt";

            //  читаем файл
            string[] text = FileReader(raw_file_path);

            //  вызываем свап порядка строк
            List<string> res = SwapSentences(text);

            //  вывод предложений в консоль в обратном порядке
            Console.WriteLine("Res: ");
            foreach (string s in res)
                Console.WriteLine(s);

            //  путь к файлу с сырым текстом
            const string res_file_path = "..\\..\\NewText.txt";

            //  вызываем запись нового порядка предложений в файл
            FileWriter(res, res_file_path);

            //  открываем файл в Win
            Process.Start(res_file_path);
        }

        static string[] FileReader(string FilePath)
        {
            //  param:
            //  string FilePath - путь к файлу
            //  return:
            //  string[] res - массив предложений

            string text = "";

            try
            {
                using (StreamReader fr = new StreamReader(FilePath))
                {
                    //  полностью считываем файл в переменную
                    text = fr.ReadToEnd();
                }
            }
            //  ловим выкидыши
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //  считываем в массив все предложения через кастомный разделитель
            //  делаем кастомный разделитель через перегрузку String.Split
            string[] res = text.Split(new string[] { ". " }, StringSplitOptions.None);

            //  ко все предложениям кроме последнего добавляем точку
            for (int i = 0; i < res.Length - 1; i++)
                res[i] += ".";

            return res;
        }

        static List<string> SwapSentences(string[] text)
        {
            //  param:
            //  string[] text - массив предложений из файла
            //  return:
            //  List<string> res - список предложений в обратном порядке

            //  создаем объект списка с предложениями
            List<string> res = new List<string>();

            //  записываем в num номер последнего элемента массива
            int num = text.Length - 1;

            //  записываем в список предложения в обратном порядке
            while (num >= 0)
            {
                res.Add(text[num]);
                num--;
            }

            return res;
        }

        static void FileWriter(List<string> text, string FilePath)
        {
            //  param:
            //  List<string> text - список предложений в обратном порядке
            //  string FilePath - путь к файлу для записи

            try
            {
                //  явно указываем, что файл всегда перезаписываем
                using (StreamWriter fw = new StreamWriter(FilePath, false))
                {
                    //  записываем список в файл
                    foreach (string line in text)
                        fw.Write(line + " ");
                }
            }
            //  ловим выкидыши
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
