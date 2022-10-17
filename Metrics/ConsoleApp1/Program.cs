using System.Diagnostics;

PerformanceCounterCategory category = new PerformanceCounterCategory(".NET CLR Memory");
String[] instancename = category.GetInstanceNames();

foreach (string name in instancename)
{
    Console.WriteLine(name);
}
Console.ReadLine();