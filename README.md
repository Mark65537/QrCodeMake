![image](https://github.com/Mark65537/QrCodeMake/blob/master/screens/main.png)
# Описание 
программа созданна для того что бы создавать множество QR-кодов 
по данным из Excel 
и отправлять их письмом по заранее созданну шаблону(Word) на электронную почту 

# Описание программы

![image](https://github.com/Mark65537/QrCodeMake/blob/master/screens/mainDesc.png)

# Описание проектов
QrCodeMakelib - проект где собранны основные классы, которые используются в остальных проектах. Для удобства
все классы являются статическими, что бы было просто импортировать в другие проекты<br>
QrCodeMake-Console - консольная версия приложения. Применяется для быстарой обработки через аргументы(на данный момент не работает)<br>
QrCodeMake-WinForm - версия c графическим интерфейсом для удобной работы с QR-кодами.
В отличие от консольной версии, не имеет аргументов при вызове<br>

# Инструкция

1. сначало нужно загрузить файл с данными пользователей в формате excel. Если у вас уже есть этот файл,
то переходите к шагу 2, если нету, то создайте файл excel в формате xlsx по заданным правилам
	1.1 первая строка не учитывается, так как в ей должны содержаться названия столбцов
	1.2 всего учитывается 5 столбцов. Первые 4 столбца конкатенируются - это и будет текст в QR-коде. 5й столбец 
		это название почты на которую будет отправленно письмо
	1.3 программа считывает файл построчно, до тех пор пока не обнаружиться пустая строка, точнее пустой 
		1й столбец в этой строке
2. Если вы хотите просто скопировать QR-код, то нажмите на кнопку "Скопировать QR-код" 
и он сразу скопируется в буфер обмена. Если вы хотите отправить выбранный QR-код или все QR-коды по Email,
то переходите к шагу 3
3. Приготовьте шаблон в формате word(.docx) как будет выглядеть ваше письмо. В шаблоне замените места которые 
будут у вас меняться в зависимости от обстоятельств. Можно заменять любыми словами, но рекомендуется 
использовать знак $, так как он редко встречаеться в обычном тексте, например: $curdate. Исключение 
составляет лишь вставка QR-кода на его месте должна быть вставка {$img_qrcode}. Все эти замены нам понадобяться 
на следующем шаге
4. Создайте файл конфигурации conf.cfg или используйте уже существующий. В него мы будем записывать все что 
заменили на предыдущем шаге. Шаблон замены такой $словоКотороеЗаменили=тоНаЧтоЗамениться 
5. В файле QrCodeMake.exe.config в теге <userSettings> содержаться настройки для отправки email, а именно
mailFrom- Имя почты с которой будет отравляться письмо
appPass- Пароль приложения, нужен для того что бы письмо отправилось. ВНИМАНИЕ: пароль приложения не является 
паролем от вашей почты. Что бы узнать пароль приложения зайдите в настройки вашей почты.
subject- Тема сообщения. по умолчанию это: "Сообщение сгенерированно с помощью программы QrCodeMake"
provider- Это некий smtp клиент который зависит от почты с которой вы отправляете.
по умолчанию стоит yandex, но вы можете заменить на ваш клиент наример: google или mail.
6. Если все сделанно правильно, то после нажатия на кнопку "Отправить по email" появиться окно 
![image](https://github.com/Mark65537/QrCodeMake/blob/master/screens/result.png)

# Описание работы программы
Программа поделена на 5 этапов<br>
1. Excel - данные загружаются из Excel и собираются в одну сущьность называемую Person(ExcelClass);
2. Form - данные из Person собранные на шаге 1 передаются на форму, где там же и отображаются, и засчет этого
строится QR-код. После нажатия на кнопку "Отправить по email" начинается шаг 3
3. Word - считывается шаблон из файла Word и преобразуется в html формат для отправки(WordClass)
4. Html - считывается файл конфига(conf.cfg)(HtmlClass)
5. Email - если существует файл конфига, то с помощью него редактируется файл html созданный на шаге 3, 
и после этого измененный html файл отправляется по почте.