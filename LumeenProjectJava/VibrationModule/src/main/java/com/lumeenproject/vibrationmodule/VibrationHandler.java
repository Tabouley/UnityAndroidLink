package com.lumeenproject.vibrationmodule;

import android.content.Context;
import android.os.Build;
import android.os.VibrationEffect;
import android.os.Vibrator;


public class VibrationHandler
{
    //Called from Unity to make the device Vibrate
    public void vibrate(Context context)
    {
        //Reference the vibrator service
        Vibrator vibrator = (Vibrator) context.getSystemService(Context.VIBRATOR_SERVICE);
        //Test the SDK to see which method use ti make the device vibrate
        if(Build.VERSION.SDK_INT>=26)
        {
            vibrator.vibrate(VibrationEffect.createOneShot(300,VibrationEffect.DEFAULT_AMPLITUDE));
        }
        else
        {
            vibrator.vibrate(300);
        }
    }
}
