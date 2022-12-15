

namespace Service

  {
    public class ServiceAccountImp : Service.AccountService
    {   


        Dictionary<int , Service.Account> dics = new Dictionary<int, Account> ();

       public  ServiceAccountImp (){

        }
        void AccountService.AddNewAccount(Account ac)
        {
            dics.Add(ac.id,ac);
        }

        Account AccountService.GetAccountById(int id)
        {
            return dics[id];
        }

        List<Account> AccountService.GetAllAccounts()
        {
           return dics.Values.ToList();
        }

        double AccountService.GetBalanceAVG()
        {
            return dics.Values.Average(acc=> acc.balance);
        }

       List<Account> AccountService.GetDebitedAccounts()
        {
            return (List<Account>) dics.Values.Where(acc => acc.balance> 0);
        }
    }

}