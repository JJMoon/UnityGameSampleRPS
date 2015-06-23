using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
    class ItemSpriteValue
    {
        public string SpriteName, Value;
    }
    ItemSpriteValue ItemSprite;
    Dictionary<string,ItemSpriteValue> ListItemCode = new Dictionary<string,ItemSpriteValue> ();

    void MailItem (string ItemCode, GameObject Gobj)
    {

    }

    void MailItemInit ()
    {

        Ag.LogDouble ("  MenuManager _ Mail Item List .... ");

        AddItemCode ("BlueDrink", "icon_bluedrink", "x1");
        
        AddItemCode ("BlueDrink1", "icon_bluedrink", "x1");
        
        AddItemCode ("BlueDrink3", "icon_bluedrink", "x3");
        
        AddItemCode ("BlueDrink5", "icon_bluedrink", "x5");
        
        AddItemCode ("BlueDrink10", "icon_bluedrink", "x10");
        
        AddItemCode ("GreenDrink", "icon_greendrink", "x1");
        
        AddItemCode ("GreenDrink1", "icon_greendrink", "x1");
        
        AddItemCode ("GreenDrink3", "icon_greendrink", "x3");
        
        AddItemCode ("GreenDrink5", "icon_greendrink", "x5");
        
        AddItemCode ("GreenDrink10", "icon_greendrink", "x10");
        
        AddItemCode ("RedDrink", "icon_reddrink", "x1");
        
        AddItemCode ("RedDrink1", "icon_reddrink", "x1");
        
        AddItemCode ("RedDrink3", "icon_reddrink", "x3");
        
        AddItemCode ("RedDrink5", "icon_reddrink", "x5");
        
        AddItemCode ("RedDrink10", "icon_reddrink", "x10");
        
        AddItemCode ("EventCash1", "icon_cash", "x1");
        
        AddItemCode ("EventCash2", "icon_cash", "x2");
        
        AddItemCode ("EventCash3", "icon_cash", "x3");
        
        AddItemCode ("EventCash5", "icon_cash", "x5");
        
        AddItemCode ("EventCash10", "icon_cash", "x10");
        
        AddItemCode ("EventCash30", "icon_cash", "x30");
        
        AddItemCode ("EventCash50", "icon_cash", "x50");
        
        AddItemCode ("Gold100", "icon_coin", "x100");
        
        AddItemCode ("Gold200", "icon_coin", "x200");
        
        AddItemCode ("Gold300", "icon_coin", "x300");
        
        AddItemCode ("Gold500", "icon_coin", "x500");
        
        AddItemCode ("Gold1000", "icon_coin", "x1000");
        
        AddItemCode ("Gold2000", "icon_coin", "x2000");
        
        AddItemCode ("Gold3000", "icon_coin", "x3000");
        
        AddItemCode ("Gold5000", "icon_coin", "x5000");
        
        AddItemCode ("Gold5500", "icon_coin", "x5500");
        
        AddItemCode ("Gold12000", "icon_coin", "x12000");
        
        AddItemCode ("Gold39000", "icon_coin", "x39000");
        
        AddItemCode ("Gold70000", "icon_coin", "x70000");
        
        AddItemCode ("Gold150000", "icon_coin", "x150000");
        
        AddItemCode ("GloveFreeWeek", "icon_heart", "x1"); // 삭제하셔도 됩니다.
        
        AddItemCode ("GloveFreeMonth", "icon_heart", "x1"); // 삭제하셔도 됩니다.
        
        AddItemCode ("HEART3", "icon_heart", "x3"); // 삭제하셔도 됩니다.
        
        AddItemCode ("HEART4", "icon_heart", "x4"); // 삭제하셔도 됩니다.
        
        AddItemCode ("HEART5", "icon_heart", "x5"); // 삭제하셔도 됩니다.
        
        AddItemCode ("TicketNormal1", "icon_card", "x1");
        
        AddItemCode ("TicketNormal2", "icon_card", "x2");
        
        AddItemCode ("TicketNormal3", "icon_card", "x3");
        
        AddItemCode ("TicketPremium1", "icon_card", "x1");
        
        AddItemCode ("TicketPremium2", "icon_card", "x2");
        
        AddItemCode ("TicketPremium3", "icon_card", "x3");
        
        AddItemCode ("CardA", "icon_card", "x1");
        
        // 추가해주세요. (운영툴에서도 카드 지급시에는 한번에 1개만 되도록 수정하였습니다.)
        
        AddItemCode ("CardB", "icon_card", "x1");
        
        AddItemCode ("CardC", "icon_card", "x1");
        
        AddItemCode ("CardD", "icon_card", "x1");
        
        AddItemCode ("CardS", "icon_card", "x1");
        
        // 추가해주세요. (조합행운권입니다. 아이콘은 빈문자로 우선 해두었습니다.)
        
        // 이벤트로 지급 설정할 수 있는 조합행운권 입니다.
        
        AddItemCode ("CardCombiAdvt1", "icon_fotune2", "x1");
        
        AddItemCode ("CardCombiAdvt3", "icon_fotune2", "x3");
        
        AddItemCode ("CardCombiAdvt5", "icon_fotune2", "x5");
        
        AddItemCode ("CardCombiAdvtHigh1", "icon_fotune1", "x1");
        
        AddItemCode ("CardCombiAdvtHigh3", "icon_fotune1", "x3");
        
        AddItemCode ("CardCombiAdvtHigh5", "icon_fotune1", "x5");
        
        AddItemCode ("CardCombiGrade1", "icon_fotune3", "x1");
        
        AddItemCode ("CardCombiGrade3", "icon_fotune3", "x3");
        
        AddItemCode ("CardCombiGrade5", "icon_fotune3", "x5");
        
        // 추가해주세요.
        
        // 팝업스토어에서 지급하는 아이템으로 “xN” 의 N 은 360 프로토콜로 전달드리는 itemCount 입니다.
            
        AddItemCode ("CardCombiAdvt", "icon_fotune2", "xN");
        
        AddItemCode ("CardCombiAdvtHigh", "icon_fotune1", "xN");
        
        AddItemCode ("CardCombiGrade", "icon_fotune3", "xN");
        
        // 추가해주세요.
        
        // 팝업스토어에서 지급하는 아이템으로 “xN” 의 N 은 360 프로토콜로 전달드리는 itemCount 입니다. 
            
        AddItemCode ("KeeperGloves01", "icon_glove1", "xN");
        
        AddItemCode ("KeeperGloves02", "icon_glove2", "xN");
        
        AddItemCode ("KeeperGloves03", "icon_glove3", "xN");
        
        AddItemCode ("KeeperGloves04", "icon_glove4", "xN");
        
        // 추가해주세요.
        
        // 팝업스토어에서 지급하는 아이템으로 “xN” 의 N 은 360 프로토콜로 전달드리는 itemCount 입니다. 
            
        AddItemCode ("KickerShoes01", "icon_shoes1", "xN");
        
        AddItemCode ("KickerShoes02", "icon_shoes2", "xN");
        
        AddItemCode ("KickerShoes03", "icon_shoes3", "xN");
        
        AddItemCode ("KickerShoes04", "icon_shoes4", "xN");


        // 300캐쉬 (조이플 프로모션 - 결제 보너스 이벤트 보상)
        AddItemCode("EventCash300", "icon_cash", "x300");

        // 1000캐쉬 (조이플 프로모션 - 결제 추첨 이벤트, 타임 추천 이벤트 보상)
        AddItemCode("EventCash1000", "icon_cash", "x1000");

        // 고급선수영입권 10개 (조이플 프로모션 - 타임 추천 이벤트 보상)
        AddItemCode("CardCombiAdvtHigh10", "icon_fotune1", "x10");

        AddItemCode ("TicketPremium10", "icon_card", "x10");

    }

    void AddItemCode (string ItemCode, string Spritename, string Itemvalue)
    {
        ItemSprite = new ItemSpriteValue ();
        ItemSprite.SpriteName = Spritename;
        ItemSprite.Value = Itemvalue;
        ListItemCode.Add (ItemCode, ItemSprite);
    }

    bool UseItemCount (string pItemTypeId) {
        if (pItemTypeId == "CardCombiAdvt" || pItemTypeId == "CardCombiAdvtHigh" || pItemTypeId == "CardCombiGrade" ||
            pItemTypeId == "KeeperGloves01" || pItemTypeId == "KeeperGloves02" || pItemTypeId == "KeeperGloves03" || pItemTypeId == "KeeperGloves04" ||
            pItemTypeId == "KickerShoes01" || pItemTypeId == "KickerShoes02" || pItemTypeId == "KickerShoes03" || pItemTypeId == "KickerShoes04" || pItemTypeId == "TicketPremium")
            return true;
        else
            return false;
    }
    
}
