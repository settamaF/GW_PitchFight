#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class PlaylistItem : IDisposable {
  private IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal PlaylistItem(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static IntPtr getCPtr(PlaylistItem obj) {
    return (obj == null) ? IntPtr.Zero : obj.swigCPtr;
  }

  ~PlaylistItem() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_PlaylistItem(swigCPtr);
        }
        swigCPtr = IntPtr.Zero;
      }
      GC.SuppressFinalize(this);
    }
  }

  public PlaylistItem() : this(AkSoundEnginePINVOKE.CSharp_new_PlaylistItem__SWIG_0(), true) {

  }

  public PlaylistItem(PlaylistItem in_rCopy) : this(AkSoundEnginePINVOKE.CSharp_new_PlaylistItem__SWIG_1(PlaylistItem.getCPtr(in_rCopy)), true) {

  }

  public PlaylistItem Assign(PlaylistItem in_rCopy) {
    PlaylistItem ret = new PlaylistItem(AkSoundEnginePINVOKE.CSharp_PlaylistItem_Assign(swigCPtr, PlaylistItem.getCPtr(in_rCopy)), false);

    return ret;
  }

  public bool IsEqualTo(PlaylistItem in_rCopy) {
    bool ret = AkSoundEnginePINVOKE.CSharp_PlaylistItem_IsEqualTo(swigCPtr, PlaylistItem.getCPtr(in_rCopy));

    return ret;
  }

  public AKRESULT SetExternalSources(uint in_nExternalSrc, AkExternalSourceInfo in_pExternalSrc) {
    AKRESULT ret = (AKRESULT)AkSoundEnginePINVOKE.CSharp_PlaylistItem_SetExternalSources(swigCPtr, in_nExternalSrc, AkExternalSourceInfo.getCPtr(in_pExternalSrc));

    return ret;
  }

  public uint audioNodeID {
    set {
      AkSoundEnginePINVOKE.CSharp_PlaylistItem_audioNodeID_set(swigCPtr, value);

    } 
    get {
      uint ret = AkSoundEnginePINVOKE.CSharp_PlaylistItem_audioNodeID_get(swigCPtr);

      return ret;
    } 
  }

  public int msDelay {
    set {
      AkSoundEnginePINVOKE.CSharp_PlaylistItem_msDelay_set(swigCPtr, value);

    } 
    get {
      int ret = AkSoundEnginePINVOKE.CSharp_PlaylistItem_msDelay_get(swigCPtr);

      return ret;
    } 
  }

  public IntPtr pCustomInfo { set { AkSoundEnginePINVOKE.CSharp_PlaylistItem_pCustomInfo_set(swigCPtr, value);
 }  get { return AkSoundEnginePINVOKE.CSharp_PlaylistItem_pCustomInfo_get(swigCPtr);
 }
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
