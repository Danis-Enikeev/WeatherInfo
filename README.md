# WeatherInfo

Для подключения к БД Microsoft SQL Server необходимо в папке InfoWeather создать файл ConnectionStrings.config со следующим cодержанием:
```
<connectionStrings>
  <add name="WeatherDBModelContainer" connectionString="" />
</connectionStrings>
```
В атрибуте ```connectionString``` нужно указать строку подключения к БД
