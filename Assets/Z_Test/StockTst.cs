using System;

public class Stock
{
    
    //declare a delegate for the event
    public delegate void AskPriceDelegate (object aPrice);
    //declare the event using the delegate
    public event AskPriceDelegate AskPriceChangedDlgt;
    
    //instance variable for ask price
    object _askPrice;
    
    //property for ask price
    public object AskPrice {
        
        set { 
            //set the instance variable
            _askPrice = value; 
            
            //fire the event
            AskPriceChangedDlgt (_askPrice); // Fire !!! 
        }
        
    }

    // Test ...  

    // Scene Drawing ...
    // Packet Design ... 

    
}//Stock class

//represents the user interface in the application
public class StockDisplay
{
    
    public void AskPriceChanged (object aPrice)
    {
        Console.Write ("The new ask price is:" + aPrice + "\r\n");
    }
    
}//StockDispslay class

public class AFunction
{

    public void TestFunction ()
    {
        Ag.LogDouble (" Test Function >>>>  ");
        //create new display and stock instances
        StockDisplay stockDisplay = new StockDisplay ();
        Stock stockObj = new Stock ();

        //create a new delegate instance and bind it
        //to the observer's askpricechanged method
        Stock.AskPriceDelegate aDelegate = new Stock.AskPriceDelegate (stockDisplay.AskPriceChanged);
        // DlgtType dlgtObject = new DlgtType ( function );  // 대리자 객체 생성. 

        //add the delegate to the event
        stockObj.AskPriceChangedDlgt += aDelegate; 
        // anInstance.aMember += dlgtObject ; 

        //loop 100 times and modify the ask price
        for (int looper=0; looper < 100; looper++) {
            stockObj.AskPrice = looper;
        }

        //remove the delegate from the event
        stockObj.AskPriceChangedDlgt -= aDelegate;
    }



}
