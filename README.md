# TotallyMoney

The solution contain the codes and tests for **Customer Preference Centre**.

## How to use 

Requirements:
- .Net 5 SDK/Runtime: Can be downloaded from https://dotnet.microsoft.com/download/dotnet/5.0 

The entry point to the solution is CustomerMarketing.exe and it need a configuration file named `shedule.csv` in the same directory as input.

The input file can have multiple rows and each row might have one of the following structures:

```text
CustomerName|Schedule|Extra Information(If its applicable)
```

Examples:

```text
Alice|Daily
John|Monthly|10,12
Edward|Never
Vicky|Weekly|Sunday,Monday,Friday
```

By adjusting the input file and running the console application, the following output will be generated:

```text
Sat 10-April-2021       Alice, John
Sun 11-April-2021       Alice, Vicky
Mon 12-April-2021       Alice, John, Vicky
Tue 13-April-2021       Alice
Wed 14-April-2021       Alice
Thu 15-April-2021       Alice
Fri 16-April-2021       Alice, Vicky
Sat 17-April-2021       Alice
Sun 18-April-2021       Alice, Vicky
Mon 19-April-2021       Alice, Vicky
Tue 20-April-2021       Alice
Wed 21-April-2021       Alice
Thu 22-April-2021       Alice
Fri 23-April-2021       Alice, Vicky
Sat 24-April-2021       Alice
Sun 25-April-2021       Alice, Vicky
Mon 26-April-2021       Alice, Vicky
Tue 27-April-2021       Alice
Wed 28-April-2021       Alice
Thu 29-April-2021       Alice
Fri 30-April-2021       Alice, Vicky
Sat 01-May-2021         Alice
Sun 02-May-2021         Alice, Vicky
Mon 03-May-2021         Alice, Vicky
Tue 04-May-2021         Alice
Wed 05-May-2021         Alice
Thu 06-May-2021         Alice
Fri 07-May-2021         Alice, Vicky
Sat 08-May-2021         Alice
Sun 09-May-2021         Alice, Vicky
Mon 10-May-2021         Alice, John, Vicky
Tue 11-May-2021         Alice
Wed 12-May-2021         Alice, John
Thu 13-May-2021         Alice
Fri 14-May-2021         Alice, Vicky
Sat 15-May-2021         Alice
Sun 16-May-2021         Alice, Vicky
Mon 17-May-2021         Alice, Vicky
Tue 18-May-2021         Alice
Wed 19-May-2021         Alice
Thu 20-May-2021         Alice
Fri 21-May-2021         Alice, Vicky
Sat 22-May-2021         Alice
Sun 23-May-2021         Alice, Vicky
Mon 24-May-2021         Alice, Vicky
Tue 25-May-2021         Alice
Wed 26-May-2021         Alice
Thu 27-May-2021         Alice
Fri 28-May-2021         Alice, Vicky
Sat 29-May-2021         Alice
Sun 30-May-2021         Alice, Vicky
Mon 31-May-2021         Alice, Vicky
Tue 01-June-2021        Alice
Wed 02-June-2021        Alice
Thu 03-June-2021        Alice
Fri 04-June-2021        Alice, Vicky
Sat 05-June-2021        Alice
Sun 06-June-2021        Alice, Vicky
Mon 07-June-2021        Alice, Vicky
Tue 08-June-2021        Alice
Wed 09-June-2021        Alice
Thu 10-June-2021        Alice, John
Fri 11-June-2021        Alice, Vicky
Sat 12-June-2021        Alice, John
Sun 13-June-2021        Alice, Vicky
Mon 14-June-2021        Alice, Vicky
Tue 15-June-2021        Alice
Wed 16-June-2021        Alice
Thu 17-June-2021        Alice
Fri 18-June-2021        Alice, Vicky
Sat 19-June-2021        Alice
Sun 20-June-2021        Alice, Vicky
Mon 21-June-2021        Alice, Vicky
Tue 22-June-2021        Alice
Wed 23-June-2021        Alice
Thu 24-June-2021        Alice
Fri 25-June-2021        Alice, Vicky
Sat 26-June-2021        Alice
Sun 27-June-2021        Alice, Vicky
Mon 28-June-2021        Alice, Vicky
Tue 29-June-2021        Alice
Wed 30-June-2021        Alice
Thu 01-July-2021        Alice
Fri 02-July-2021        Alice, Vicky
Sat 03-July-2021        Alice
Sun 04-July-2021        Alice, Vicky
Mon 05-July-2021        Alice, Vicky
Tue 06-July-2021        Alice
Wed 07-July-2021        Alice
Thu 08-July-2021        Alice

```

## How to develop

Clone the repository in Visual Studio or Jetbrains Rider and start developing.

## Future Improvement
- Enable getting the schedule.csv file from commandline
- Enable the whole input operations from commandline 
- Enable multiple types of subscription for each customer
- Better design on input validation.
