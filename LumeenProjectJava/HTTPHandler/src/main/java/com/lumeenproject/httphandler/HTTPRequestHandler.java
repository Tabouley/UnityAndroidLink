package com.lumeenproject.httphandler;

public class HTTPRequestHandler
{
    /**
     * Called from Unity to fetch joke through HTTP request
     * @param cb : callback instance
     */

    public void requestJoke(HTTPRequestCallback cb)
    {
        //Start an async task to request joke with HTTP
        new HTTPRequestAsyncTask(cb).execute("https://official-joke-api.appspot.com/random_joke");
    }
}
