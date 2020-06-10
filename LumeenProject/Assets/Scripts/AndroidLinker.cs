using UnityEngine;
using UnityEngine.UI;
public class AndroidLinker : MonoBehaviour
{
    // Reference UnityPlayer current activity
    AndroidJavaObject currentActivity;
    // String updated from callback
    public string jokeString="";
    // Reference the Text component where the jokes are displayed
    public Text jokeText;

    /**
     * Called when the script instance is being loaded and fetch UnityPlayer current activity then reference it
     */
    void Awake()
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }
    /**
     * Display a toast notification on the device with param:msg as the message displayed
     */
    public void callToast(string msg)
    {
        AndroidJavaObject appContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        //Fetch Widget Toast class
        AndroidJavaClass ToastClass = new AndroidJavaClass("android.widget.Toast");
        //Convert string msg to Java String 
        AndroidJavaObject msgString = new AndroidJavaObject("java.lang.String", msg);
        //Create the toast notification
        AndroidJavaObject toastObject = ToastClass.CallStatic<AndroidJavaObject>("makeText", appContext, msgString, ToastClass.GetStatic<int>("LENGTH_SHORT")); ;
        // Show the toast notification
        toastObject.Call("show");
    }
    /**
     * Request a joke from async HTTP request through plugin and instantiate the callback
     */
    public void callJoke()
    {
        //Instantiating Plugin HTTPRequest Handler
        AndroidJavaObject HTTPRequestHandler = new AndroidJavaObject("com.lumeenproject.httphandler.HTTPRequestHandler");
        //Call the requestJoke(HTTPRequesthandler) function in Plugin to request a new joke
        HTTPRequestHandler.Call("requestJoke",new AndroidCallback(this));

    }

    /**
     * Make the device vibrate
     */
    public void callVibration()
    {
        //Instantiating plugin VibrationHandler
        AndroidJavaObject vibrate = new AndroidJavaObject("com.lumeenproject.vibrationmodule.VibrationHandler");
        //Call the Vibrate(Context) function in Plugin to make the device vibrate
        vibrate.Call("vibrate", currentActivity.Call<AndroidJavaObject>("getApplicationContext"));
    }

    /**
     * Called every frame
     */
    private void Update()
    {
        //Update Text UI
        jokeText.text= jokeString;
    }
}