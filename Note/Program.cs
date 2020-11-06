using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NoteBook
{
    class Program
    {
        static void Main(string[] args)
        {
            NoteBook noteBook = new NoteBook();
            noteBook.Menu("Это Записная Книжка. Поля имеющие  * и голубой цвет являются обязательными, их пропустить нельзя");

        }
    }
    public class Note
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Otchestvo { get; set; }
        public int NumberTel { get; set; }
        public string Countre { get; set; }
        public string Commetnt { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }


        public Note(string name, string lastName, int number_t, string countre)
        {
            Name = name;
            LastName = lastName;
            NumberTel = number_t;
            Countre = countre;
        }

        public override string ToString()
        {
            return "Имя: " + Name + "\n" + "Фамилия: " + LastName + "\n" + "Номер телефона: " + NumberTel + "\n" + "Страна: " + Countre + "\n";
        }

    }

    public class NoteBook
    {
        private readonly List<Note> human;

        public NoteBook()
        {
            human = new List<Note>();
        }

        private T GetData<T>(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(message);
            var data = Console.ReadLine();
            if (typeof(T) == typeof(int))
            {
                int result2 = 0;
                int number = 0;
                bool exit = true;
                do
                {
                    if (!Int32.TryParse(data, out result2) || result2 < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (data == "" || data == null)
                            Console.WriteLine("Это обязательное поле, его нельзя пропустить!");
                        else
                            Console.WriteLine("Неприемлемые символы!");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(message);
                        data = Console.ReadLine();
                    }
                    else
                    {
                        int count = 0;
                        number = result2;
                        while (result2 != 0)
                        {
                            count++;
                            result2 /= 10;
                        }
                        if (count != 8)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Неверно набран номер, номер  должен содержать 8 цифр! ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(message);
                            data = Console.ReadLine();
                        }
                        else
                            exit = false;

                    }

                } while (exit);

                return (T)Convert.ChangeType(number, typeof(T));
            }
            else

            {
                while (!Regex.Match(data, "^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$").Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (data == "" || data == null)
                        Console.WriteLine("Это обязательное поле, его нельзя пропустить!");
                    else
                        Console.WriteLine("Ошибка! Неверные символы! Должны начинаться с заглавной буквы! И содержать только буквы");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(message);
                    data = Console.ReadLine();
                }
                return (T)Convert.ChangeType(data, typeof(T));

            }
        }
        private void AddNote()
        {

            var name = GetData<string>("*Ведите Имя! Наименование должно начинаться с ЗАГЛАВНОЙ буквы: ");
            var lastName = GetData<string>("*Введите фамилию! Наименование должно начинаться с ЗАГЛАВНОЙ буквы: ");
            var otchestvo = GetData2("Введите отчество или пропустите: ");
            var number_t = GetData<int>("*Ведите номер телефона(только цифры): ");
            var countre = GetData<string>("*Введите страну! Наименование должно начинаться с ЗАГЛАВНОЙ буквы: ");
            var date = Date("Введите дату рождения (д.мм.гггг)");
            var organization = GetData2("Ведите организацию или пропустите: ");
            var positsion = GetData2("Введите должность или пропустите: ");
            var comment = GetDataComment("Введите коментарий или пропустите: ");


            human.Add(new Note(name, lastName, number_t, countre) { Otchestvo = otchestvo, Organization = organization, Position = positsion, Commetnt = comment, Date = date });
        }
        private void ShowNotes()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (human.Count > 0)
                human.ForEach(n => Console.WriteLine(n));
            else
                Console.WriteLine("Пока нет ни одной записи");
        }

        private string GetData2(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            var data = Console.ReadLine();

            while (data != "" && !Regex.Match(data, "^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$").Success)
            {
                Console.WriteLine("Ошибка!");
                Console.Write(message);
                data = Console.ReadLine();

            }
            return data;

        }
        private string GetDataComment(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            var data = Console.ReadLine();
            return data;
        }

        public void Menu(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            bool a = true;
            while (a)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nВведите команду: 1 - Создания новой учетной записи; 2 - Все учетные записи; 3 - Показать запись по Индексу; 4 - Показать запись по Фамилии; 5 - Редактировать запись; 6 - Удаление записи; 7 - Выход");
                string number = Console.ReadLine();
                if (Int32.TryParse(number, out int i) && i < 8)
                {
                    switch (i)
                    {
                        case 1:
                            AddNote();
                            break;

                        case 2:
                            ShowNotes();
                            break;
                        case 3:
                            ShowNote_Index();
                            break;
                        case 4:
                            ShowNote_LastName();
                            break;
                        case 5:
                            EditNote();
                            break;
                        case 6:
                            DeleteNote();
                            break;
                        case 7:
                            a = false;
                            break;
                    }
                }
                else
                    Console.WriteLine("Введите число из представленных!");
            }

        }

        private void ShowNote_Index()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (human.Count != 0)
            {
                Console.WriteLine($"Всего записей {human.Count} выберете одну из {human.Count} начиная с 0");
                var i = Int32.Parse(Console.ReadLine());
                Console.WriteLine($"Имя: {human[i].Name} \nФамилия: {human[i].LastName} \nОтчество:{human[i].Otchestvo} \nНомер телефона: {human[i].NumberTel} \nСтрана: {human[i].Countre} \nОрганизация: {human[i].Organization} \nДолжность: {human[i].Position} \nКоментарий: {human[i].Commetnt}\nДата рождения: {human[i].Date}  ");
            }
            else
            {
                Console.WriteLine("Пока нет ни одной записи");
            }

        }
        private void ShowNote_LastName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (human.Count != 0)
            {
                Console.WriteLine("Введите фамилию человека с заглавной буквы");
                var a = Console.ReadLine();
                bool c = false;
                for (var i = 0; i < human.Count; i++)
                {
                    if (human[i].LastName == a)
                    {
                        Console.WriteLine($"Имя: {human[i].Name} \nФамилия: {human[i].LastName} \nОтчество: {human[i].Otchestvo} \nНомер телефона: {human[i].NumberTel} \nСтрана: {human[i].Countre} \nОрганизация: {human[i].Organization} \nДолжность: {human[i].Position} \nКомментарий: {human[i].Commetnt}\nДата Рождения: {human[i].Date} ");
                        c = true;
                    }
                }
                if (c == false)
                    Console.WriteLine("Ничего не найденно!");

            }
            else
            {
                Console.WriteLine("Пока нет ни одной записи");
            }

        }

        private void EditNote()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (human.Count != 0)
            {
                Console.WriteLine("Введите фамилию человека с заглавной буквы чьи данные хотите редактировать!");
                var a = Console.ReadLine();
                var c = false;
                var exit = true;
                for (int i = 0; i < human.Count; i++)
                {
                    if (human[i].LastName == a)
                    {
                        while (exit)
                        {
                            Console.WriteLine("Выберете поле для редоктирования: 1-Имя; 2-Фамилия; 3-Отчество; 4-Номер телефона; 5-Страна; 6-Организация; 7-Должность; 8-Комментарий; 9-Дата рождения; 10-НАЗАД В МЕНЮ");
                            string edit = Console.ReadLine();
                            if (Int32.TryParse(edit, out int result))
                            {
                                switch (result)
                                {
                                    case 1:
                                        human[i].Name = GetData<string>("Введите новое Имя: ");
                                        break;
                                    case 2:
                                        human[i].LastName = GetData<string>("Введите новую Фамилию: ");
                                        break;
                                    case 3:
                                        human[i].Otchestvo = GetData2("Введите отчество: ");
                                        break;
                                    case 4:
                                        human[i].NumberTel = GetData<int>("Введите новый номер телефона: ");
                                        break;
                                    case 5:
                                        human[i].Countre = GetData<string>("Введите новую страну: ");
                                        break;
                                    case 6:
                                        human[i].Organization = GetData2("Введите новую организацию: ");
                                        break;
                                    case 7:
                                        human[i].Position = GetData2("Введите новую должность: ");
                                        break;
                                    case 8:
                                        human[i].Commetnt = GetData2("Введите новый комментарий: ");
                                        break;
                                    case 9:
                                        human[i].Date = Date("Введите новую дату рождения: ");
                                        break;
                                    case 10:
                                        exit = false;
                                        break;


                                }
                            }
                            Console.WriteLine("Введите одно из представленных чисел!");
                        }
                        c = true;
                    }
                }
                if (c == false)
                    Console.WriteLine("Ничего не найденно!");

            }
            else
            {
                Console.WriteLine("Пока нет ни одной записи");
            }


        }

        private void DeleteNote()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (human.Count != 0)
            {
                Console.WriteLine("Введите фамилию человека чью запись хотите удалить");
                string a = Console.ReadLine();
                bool exit = false;
                for (int i = 0; i < human.Count; i++)
                {
                    if (human[i].LastName == a)
                    {
                        human.RemoveAt(i);

                        Console.WriteLine($"\n Запись успешно удалина! ");
                        exit = true;
                    }
                }
                if (exit == false)
                    Console.WriteLine("Ничего не найденно!");
            }
            else
            {
                Console.WriteLine("Пока нет ни одной записи");
            }

        }
        private DateTime Date(string message)
        {
            var day = 0;
            var month = 0;
            var year = 0;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            Console.Write("Введите день или пропустите: ");
            string date = Console.ReadLine();
            if (date == "" || date == null)
                return new DateTime();
            else
            {
                bool exit1 = true;

                do
                {
                    if (!Int32.TryParse(date, out day) || day < 1 || day > 31)
                    {
                        Console.WriteLine("Неприемлемые символы!");

                        Console.Write("Введите день корректно: ");
                        date = Console.ReadLine();
                    }
                    else
                        exit1 = false;
                } while (exit1);

                bool exit2 = true;
                Console.Write("Введите месяц: ");
                string date2 = Console.ReadLine();
                do
                {
                    if (!Int32.TryParse(date2, out month) || month < 1 || month > 12)
                    {
                        Console.WriteLine("Неприемлемые символы!");

                        Console.Write("Введите месяц корректно: ");
                        date2 = Console.ReadLine();
                    }
                    else
                        exit2 = false;
                } while (exit2);

                bool exit3 = true;
                Console.Write("Введите год: ");
                string date3 = Console.ReadLine();
                do
                {
                    if (!Int32.TryParse(date3, out year) || year < 1910 || year > 2020)
                    {
                        Console.WriteLine("Неприемлемые символы!");

                        Console.Write("Введите год корректно: ");
                        date3 = Console.ReadLine();
                    }
                    else
                        exit3 = false;
                } while (exit3);

                return new DateTime(year, month, day);

            }

        }

    }

}