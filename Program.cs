// See https://aka.ms/new-console-template for more information
// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();
DateTime today = DateTime.Now;


if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new Random();
    // create file
    StreamWriter sw = new StreamWriter("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week and then a sum and average
        List<int> hours = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours.Add(rnd.Next(4, 13));
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours) + "|" + hours.Sum() + "|"}{((double)hours.Sum() / hours.Count()):f1}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    // TODO: parse data file
    string txtReadOut = new StreamReader("data.txt").ReadToEnd();;

    List<string> lines = new List<string>(txtReadOut.Split("\n").SkipLast(1));
    
    foreach(string line in lines){
        if(line != File.ReadLines("data.txt").Last()){
            DateTime date = DateTime.Parse(line.Split(",")[0]); 
            string[] entryStr = line.Split(",")[1].Split("|");
            double avg = double.Parse(entryStr.Last());


            int[] entryInt = entryStr.SkipLast(1).Select(int.Parse).ToArray();

            Console.WriteLine($"\nWeek of {date:MMM}, {date:dd}, {date:yyyy}");
            Console.WriteLine($" Su Mo Tu We Th Fr Sa Tot Avg ");
            Console.WriteLine($" -- -- -- -- -- -- -- --- --- ");
            Console.WriteLine($" {entryInt[0]:D2} {entryInt[1]:D2} {entryInt[2]:D2} {entryInt[3]:D2} {entryInt[4]:D2} {entryInt[5]:D2} {entryInt[6]:D2} {entryInt[7]:D3} {avg}");
        }
    }
 
}