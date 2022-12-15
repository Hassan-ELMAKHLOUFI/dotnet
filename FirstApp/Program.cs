using Service ;

internal class Program
{
    private static void Main(string[] args)
    {

        AccountService ASI= new Service.ServiceAccountImp();


        ASI.AddNewAccount(new Account(1,"USD",50));
        ASI.AddNewAccount(new Account(2,"MAD",100));
        ASI.AddNewAccount(new Account(3,"USD",200));
        ASI.AddNewAccount(new Account(4,"MAD",300));
        ASI.AddNewAccount(new Account(5,"USD",400));


        Account ac1 = ASI.GetAccountById(1);
        Console.WriteLine("======================");
        Console.WriteLine("Affichage du compte qui a id 1 :"+ac1.ToString());

        
        double d  = ASI.GetBalanceAVG();
        Console.WriteLine("======================");
        Console.WriteLine("La moyenne des balances est :" +d);
        Console.WriteLine("======================");

        Console.WriteLine("Affichage de tous les comptes");
        List<Account>   accounts = ASI.GetAllAccounts();
        foreach(Account ac in accounts)
        Console.WriteLine(ac.ToString());
        Console.WriteLine("======================");
     }}