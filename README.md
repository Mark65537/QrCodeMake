# Описание 
программа созданна для того что бы создавать множество QR-кодов 
по данным из Excel 
и отправлять их письмом по заранее созданну шаблону(Word) на электронную почту 

# Описание программы

[!image]()

# Описание проектов
mQrCodeMake - проект где собранны основные модули, которые используются в остальных проектах. Для удобства
все модули являются статическими, что бы было просто импортировать в другие проекты<br>
QrCodeMake-Console - консольная версия приложения. Применяется для быстарой обработки через аргументы<br>
QrCodeMake-WinForm - версия c графическим интерфейсом для удобной работы с QR-кодами.
В отличие от консольной версии, не имеет аргументов при вызове<br>

# Инструкция



# Описание работы программы
Программа поделена на 5 этапов<br>
1. Excel - данные загружаются из Excel и собираются в одну сущьность называемую Person(ExcelClass);
2. Form - данные из Person собранные на шаге 1 передаются на форму, где там же и отображаются, и засчет этого
строится QR-код. После нажатия на кнопку "Отправить по email" начинается шаг 3
3. Word - считывается шаблон из файла Word и преобразуется в html формат для отправки(WordClass)
4. Html - считывается файл конфига(conf.cfg)(HtmlClass)
5. Email - если существует файл конфига, то с помощью него редактируется файл html созданный на шаге 3, 
и после этого измененный html файл отправляется по почте.