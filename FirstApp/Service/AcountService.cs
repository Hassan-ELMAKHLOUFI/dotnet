namespace Service
{
    public interface AccountService{
       public void AddNewAccount(Account ac);
        public List<Account> GetAllAccounts() ; 
        public Account  GetAccountById(int id);
        public List<Account>  GetDebitedAccounts();
        public double GetBalanceAVG() ;
   
    }
}