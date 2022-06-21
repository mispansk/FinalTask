using ConsoleApp863;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    /// <summary>
    /// Класс Студент, имеющий свойства: Имя, Группа, Дата Рождения
    /// и метод Print, который выводит данные студента
    /// </summary>
    
    [Serializable]
    
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// метод, который выводит данные студента
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"Имя: {Name   }    Группа: {Group   }    Дата рождения: {DateOfBirth}");           
        }
    }

    class Program
    {
        const string filePath = @"C:\Work\skillFactori\Students.dat"; // путь до бинарного файла
        static void Main(string[] args)
        {
            Student[] studarray;

            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var obj = formatter.Deserialize(fs); // десериализация
                studarray = (Student[])obj;
                Console.WriteLine("изначальный массив");
                PrintArray(studarray); // вывод изначального массива студентов
            }
            
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Desktop\Students");
            if (dirInfo.Exists)
                dirInfo.Delete(true); // Если папка уже существует, удалим ее и создадим такую же пустую, новоую
            Directory.CreateDirectory(@"C:\Desktop\Students");

            SortByGroup(studarray); // сортируем массив студентов по группе
            Console.WriteLine("\n\nотсортированный массив");
            PrintArray(studarray); // вывод отсорированного массива

            // сравнивание студентов по группе, отправка списка студентов с одинаковой группой в метод WriteFile
            int a;
            int b = 0;

            for ( a = 0; a < studarray.Length - 1 ; a++)
            {
                if (studarray[a].Group == studarray[a + 1].Group)
                {
                    a++;
                }
                else
                {
                    WriteFile(studarray, a, b, studarray[a].Group);
                    b = a + 1;
                }                            
            }
            WriteFile(studarray, a , b, studarray[a - 1].Group);          
        }

        /// <summary>
        /// метод, который создает файл с название группы и записывает в него студентов с этой группы
        /// </summary>
        /// <param name="students"> массив студентов </param>
        /// <param name="aa"></param>
        /// <param name="bb"></param>
        /// <param name="str"> название группы (нужно для создания файла с таким названием) </param>
        private static void WriteFile(Student[] students, int aa, int bb, string str)
        {
            string filePath = @"C:\Desktop\Students\" + str + ".txt";
            
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    for (int i = bb; i <= aa; i++)
                        sw.WriteLine(students[i].Name + "  " + students[i].DateOfBirth);
                }          
        }
              
        /// <summary>
       /// метод, который печатает массив студентов
       /// </summary>
       /// <param name="studarray"> массив студентов </param>
        private static void PrintArray(Student[] studarray)
        {
            foreach (Student st in studarray)
            {
                st.Print();
            }
            Console.ReadKey();
        }

        /// <summary>
        /// сортировка массива по грурппе
        /// </summary>
        /// <param name="students"> массив студентов </param>
        public static void SortByGroup( Student[] students)
        {
            Student a;
            for (int i = 0; i <= students.Length - 2; i++)
            {
                for (int k = 0; k <= students.Length - 2 - i; k++)
                {
                    if (students[k].Group.CompareTo(students[k+1].Group) > 0)
                    {
                        a = students[k];
                        students[k] = students[k+1];
                        students[k+1] = a;

                    }
                }
            }
        }
    }
}



