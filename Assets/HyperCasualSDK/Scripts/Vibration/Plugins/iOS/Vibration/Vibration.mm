#import <Foundation/Foundation.h>#import <AudioToolbox/AudioToolbox.h>#import <UIKit/UIKit.h>#import "Vibration.h"@implementation Vibration+ (void)vibrate {    AudioServicesPlaySystemSoundWithCompletion(1352, NULL);}+ (void)vibrateLong {    AudioServicesPlaySystemSoundWithCompletion(1519, NULL);}+ (void)vibrateShort {    AudioServicesPlaySystemSoundWithCompletion(1520, NULL);}@endextern "C" {    void VibrateApple()     {        [Vibration vibrate];    }        void VibrateLongApple()     {        [Vibration vibrateLong];    }    void VibrateShortApple()     {        [Vibration vibrateShort];    }}