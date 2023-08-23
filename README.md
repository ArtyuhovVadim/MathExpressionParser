# **MathExpressionParser**

## **Общая информация**

MathExpressionParser - библиотека для парсинга математических выражений.

## **Nuget**
[MathExpressionParser](https://www.nuget.org/packages/MathExpressionParser/) для .netstandard2.1

## **Быстрый старт**

```csharp
var parser = MathParserBuilder.BuildDefaultParser();
var output = parser.Parse("2 + 2 * 2"); // 6 
```

## **Возможности библиотеки**

### Встроенные операторы

1. \+ - сложение
1. \- - вычитание
1. \/ - деление
1. \* - умножение
1. \^ - возведение в степень
1. \/\/ - деление нацело
1. \% - деление с остатком
1. \( \) - измение приоритета операций

### Синтаксис вызова функций

```
some_func(arg1, arg2, arg3, ...)
```
Все функции не чувствительны к регистру, можно писат как max(...), так и MAX(...) и т.д.

### Синтаксис использования констант

```
pi
e
max
min
```
Все константы не чувствительны к регистру, можно писат как pi, так и PI и т.д.

### Запись чисел

1. `42069` - десятичное число
1. `0xFBF` - шестнадцатеричное число
1. `0o453` - восьмеричное число
1. `0b101` - двоичное число

### Встроенные константы

1. `pi` - число пи
1. `e` - число e
1. `max` - максимальное значение числа `double` (1.7976931348623157E+308)
1. `min` - минимальное значение числа `double` (-1.7976931348623157E+308)

### Встроенные функции

1. sin(x) - синуст числа x
1. cos(x) - косинус числа x
1. tg(x) - тангенс числа x
1. ctg(x) - котангенс числа x
1. arcsin(x) - арксинус числа x
1. arccos(x) - арккосинус числа x
1. arctg(x) - арктангенс числа x
1. arcctg(x) - арккотангенс числа x
1. pow(x, y) - число x возведенное в степеь y
1. asb(x) - модуль числа x
1. sqrt(x) - квадратный корень числа x
1. log(x, y) - логарифм числа x по основанию y
1. lg(x) - десятичный логарифм числа x
1. ln(x) - натуральный логарифм числа x
1. exp(x) - экспонента числа x
1. sign(x) - возвращает знак числа (-1 если число меньше 0, 1 если больше 0, 0 если число равно 0)
1. d2r(x) - переводит число x в градусах в радианы
1. r2d(x) - переводит число x в радианах в градусы
1. max(x, y, ...) - возвращает максимальное число из x, y и т.д. (функция принимает 1 или больше аргументов)
1. min(x, y, ...) - возвращает минимальное число из x, y и т.д. (функция принимает 1 или больше аргументов)
1. avg(x, y, ...) - возвращает среднее число из x, y и т.д. (функция принимает 1 или больше аргументов)

### Построение не стандартного парсера

#### Создание парсера без стандартных функций и констант

```csharp
var parser = MathParserBuilder
    .Create()
    .Build();
```

#### Создание парсера со всеми встроенными функциями и всеми встроенными константами

```csharp
var parser = MathParserBuilder
    .Create()
    .WithDefaultConstants()
    .WithDefaultFunctions()
    .Build();
```

Запись выше эквивалента данной записи
```csharp
var parser = MathParserBuilder.BuildDefaultParser()
```

#### Создание парсера с нестандартными функциями и константами
```csharp
var parser = MathParserBuilder
    .Create()
    .WithConstant("my_const", 54)
    .WithFunction("my_func", 2, args => (args[0] + args[1]) / 2) // кол-ко аргументов строго 2
    .WithFunction("my_func1", argsCount => argsCount >= 3, args => args.Average() / 2) // кол-ко аргументов больше или равно 3
    .Build();
```

### Пример использования

Пример консольного приложения - [TestProject](https://github.com/ArtyuhovVadim/MathExpressionParser/blob/dev/src/TestProject/Program.cs).

```csharp
var parser = MathParserBuilder.BuildDefaultParser()
var output = parser.Parse("pow(2 + 2 * 2 - sqrt(2) / cos(max(pi / 2, pi)), 2)") // 54.97
```

## **Лицензия**
Этот проект лицензирован [MIT](https://github.com/ArtyuhovVadim/MathExpressionParser/blob/dev/LICENSE).
