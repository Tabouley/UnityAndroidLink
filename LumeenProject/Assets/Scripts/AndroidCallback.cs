
using UnityEngine;

/**
 * Class implementing Java Interfae HTTPRequestCallback from HTTPHandler plugin
 * 
 */
public class AndroidCallback : AndroidJavaProxy
{
    //Referencing the AndroidLinker to update UI from this thread
    AndroidLinker linker;
    /**
     * Construct the callback and reference the AndroidLinker that called it.
     */
    public AndroidCallback(AndroidLinker lkr) : base("com.lumeenproject.httphandler.HTTPRequestCallback") { linker=lkr; }

    /**
     * Called from Plugin on Success and set the string value of Androidlinker to update UI and display result after deserializing json 
     */
    public void onSuccess(string res)
    {
        Joke joke = Joke.CreateFromJSON(res);
        linker.jokeString = joke.setup + "\n...\n" + joke.punchline;


    }
    /**
     * Called from Plugin on Error and set the string value of Androidlinker to update UI and display error message
     */
    public void onError(string errorMsg)
    {
        linker.jokeString = "ERROR : " + errorMsg;
    }

}