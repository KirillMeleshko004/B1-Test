Как запустить:

Клиентская часть и серверная часть запускаются отдельно

Для запуска серверной части:

Перейти в директорию "ExcelWorker\ExcelServer\ExcelServer.Infrastructure"
Ввести в консоль dotnet ef migrations add ChangedLengthConstraints -- "Server=localhost\SQLEXPRESS;Database=TestExcel;Trusted_Connection=True;TrustServerCertificate=True;"
Ввести в консоль dotnet ef database update -- "Server=localhost\SQLEXPRESS;Database=TestExcel;Trusted_Connection=True;TrustServerCertificate=True;"

Перейти в директорию "ExcelWorker\ExcelServer\ExcelServer.Api"
Ввести в консоль dotnet run

Серверная часть запущена по адресу "http://localhost:5257"

Для запуска клиентской части:

Перейти в директорию "ExcelWorker\excel-client"
Ввести в консоль npm install (нужен установленный npm)
Ввести в консоль npm run dev

Клиенсткая часть запущена по адресу "http://localhost:5173/"
