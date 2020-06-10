package com.lumeenproject.httphandler;

import android.os.AsyncTask;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.URL;

public class HTTPRequestAsyncTask extends AsyncTask<String, Void, Boolean>
{
    HTTPRequestCallback callback;
    public HTTPRequestAsyncTask(HTTPRequestCallback cb)
    {
        super();
        this.callback=cb;
    }

    /**
     * Execute the HTTP Request in background
     * @param _url : Size 1 Array that contains the url for the HTTPRequest
     */
    protected Boolean doInBackground(String... _url)
    {
        try
        {
            // HTTP Request
            URL url = new URL(_url[0]);
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();
            InputStream in = new BufferedInputStream(urlConnection.getInputStream());
            String res = readInput(in);
            //Send result to Unity to update display
            callback.onSuccess(res);
            return true;


        }
        //Catch exception and display it on the device through Unity
        catch(Exception e)
        {
            callback.onError(e.getMessage());
        }
        return false;
    }

    /**
     * Read all the char through inputStream
     * @param in : inputstream where the data is stocked
     * @return string that contains all the data from inputstream
     * @throws IOException : throw IO Exception to let the previous function catch it
     */
    public String readInput(InputStream in) throws IOException
    {
            int i;
            char c;
            String str ="";
            while((i = in.read())!=-1) {
                c = (char)i;
                str+=c;
            }

            return str;

    }
}
