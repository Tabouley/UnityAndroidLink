package com.lumeenproject.httphandler;


/**
 * Java interface implemented in Unity to callback to Unity with HTTPRequest result
 */
public interface HTTPRequestCallback
{
    public void onSuccess(String res);
    public void onError(String errorMsg);
}
