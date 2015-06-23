//
//  ANBeziers.h
//  ObjectPainter
//
//  Created by Jongwoo Moon on 12. 26. 2012..
//  Copyright (c) 2012년 jongwoomooon@gmail.com All rights reserved.
//
//  mht : hansoo term   mui : UI            mst : state         mtm : Thread/tiMer
//  mit : integer       mfo : float         mbl : bool          mgo : geometry
//  arr : array         dic : dictionary    mla : Layer         man : ANima
//  mdd : distance      mpo : point         mcl : color         mpa : path
//  mgn : generateObj   mtp : tempObj       moj : object        mng : manageObj
//  mgc : graphic rel.
//  updated @ : 2012. 3. 21.
//

<<<<<<< HEAD
=======
// KKO App ID :: 87801776877628656

>>>>>>> f1b8abc2c316c5860d87d52936c1e48360408c55

#import <Foundation/Foundation.h>
//#import "AppDelegate.h"

//static NSString *const mgSeparator =
//@"/////////////////////////////////////////////////////////    ->> [Xcode]     ";
//@" ->> [Xcode] ==============================================================  [Xcode] ";
static NSString *const mgXcodeMark = @" ---- [ Xcode ] ---- ";


//////////////////////////////////////////////////////////////////////////////////////////
# pragma mark - property  #### Don't forget to ADD synthesize ####

@interface AgPlugin : NSObject //UIResponder <UIApplicationDelegate>
{
    // Keeps track of available services
    NSMutableArray *arrSome;
	
    //NSDictionary *dicMyInfo;
    
    
}

//@property (retain, nonatomic) NSDictionary *dicMyInfo;

+(void) timeSendMsg;

//@property (retain, nonatomic) NSString *mtrMyID, *mtrMyNick, *mtrMyImage;


@end

static AgPlugin *gmAgPlugin = [[AgPlugin alloc] init];

/*
//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////   [ iPhone_View.mm ] MainGLView.touchesBegan  /////
double curTime = CACurrentMediaTime();
UnitySendMessage("FpLogMan", "callbackTime", [[NSString stringWithFormat:@"%f", curTime ] UTF8String] );
NSLog(@" <<<<<     [ iPhone_View.mm ] MainGLView.touchesBegan     >>>>> ");
//////////////////////////////////////   [ iPhone_View.mm ] MainGLView.touchesBegan  /////
//////////////////////////////////////////////////////////////////////////////////////////
//*/


