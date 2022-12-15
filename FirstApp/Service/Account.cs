namespace Service{ 
public class Account
{
    public double balance ;
    public  int id ;
    public string currency;
    public Account()
    {
    
    }
    public Account(int id ,string currency,double balance)
    {
        this.id = id;
        this.currency = currency ;
        
        this.balance = balance ;
    }
    public override string ToString()
    {
        return  "id "+id+ " balance "+balance ; 
    }
    
}
}