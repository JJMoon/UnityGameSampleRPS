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

#import "AgPlugin.h"

//////////////////////////////////////////////////////////////////////   [ KakaoDelegate ]
@implementation AgPlugin 
//////////////////////////////////////////////////////////////////////////////////////////

- (id)init
{
    NSLog(@"\n\n\n");
    NSLog(@"->> [Xcode] KakaoDelegate::init  \n\n\n" );
    
    //arrSome = [[NSMutableArray alloc] init];
    
    return self;
}

- (void)dealloc
{
    [super dealloc];
}

//////////////////////////////////////////////////////////////////////////////////////////
# pragma mark - Time Check
+(void) timeSendMsg{
    NSLog(@"timeSendMsg  >>>>>");
    double curTime = CACurrentMediaTime();
    NSLog(@"Cur :: %f", curTime);
    UnitySendMessage("PluginIOS", "callbackTime", [[NSString stringWithFormat:@"%f", curTime ] UTF8String] );
}


@end

/*

 // VideoViewController.mm 에 추가.
 // Class Method 로 변경함.
 
 - (void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event
{
 double curTime = CACurrentMediaTime();
 NSLog(@" >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   ");
 NSLog(@"     Xcode :: touches touchesBegan Cur ::         %f  ", curTime);
 
 [AgPlugin timeSendMsg];
 
 for (UITouch *touch in touches)
 {
 NSArray *array = touch.gestureRecognizers;
 for (UIGestureRecognizer *gesture in array)
 {
 if (gesture.enabled && [gesture isMemberOfClass:[UIPinchGestureRecognizer class]])
 gesture.enabled = NO;
}

*/

//////////////////////////////////////////////////////////////////////////////////////////
# pragma mark - extern "C" ____

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules

extern "C" {
    
    void _NativeLog(const char* pString) {
        NSLog(@"%s", pString);
    }

	
    void _TimeCheck() {
//        for (int kk=0; kk<1; kk++) {
            NSLog(@" Xcode :: extern :: _TimeCheck    >>>>>   ");
            double curTime = CACurrentMediaTime();
            UnitySendMessage("PluginIOS", "callbackTime", [[NSString stringWithFormat:@"%f", curTime ] UTF8String] );
  //      }
    }

    void _CheckJailBreak() {
        NSString *filePath = @"/Applications/Cydia.app";
        if([[NSFileManager defaultManager] fileExistsAtPath:filePath]) {
            NSLog(@"\n\n\n");
            NSLog(@"->> [Xcode] KakaoDelegate :: checkJailBreak  >>>>>>>>>>>>>>>>>>>>  !!!!  Jail Broken .... !!!   Stop All  \n" );
            NSLog(@"\n\n\n\n\n\n\n\n\n");
            UnitySendMessage("PluginIOS", "callJailBreak", "Header_*01*_Broken" );
        } else {
            NSLog(@"\n\n\n");
            NSLog(@"->> [Xcode] KakaoDelegate :: checkJailBreak  >>>>>>>>>>>>>>>>>>>   No Jail Broken :: OK  \n" );
            UnitySendMessage("PluginIOS", "callJailBreak", "Header_*01*_Safe" );
        }
    }

    void _SendMessage() {
        NSLog(@" _Send Message ");
        //[gmKakaoDlgt sendMessage];
    }
    
}






