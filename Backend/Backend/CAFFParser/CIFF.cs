//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Backend.CAFFParser {

public class CIFF : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CIFF(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CIFF obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(CIFF obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~CIFF() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          CAFFParserPINVOKE.delete_CIFF(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public static CIFF parseCIFF(BytesVector bytes, Endianess endianess) {
    CIFF ret = new CIFF(CAFFParserPINVOKE.CIFF_parseCIFF(BytesVector.getCPtr(bytes), (int)endianess), true);
    if (CAFFParserPINVOKE.SWIGPendingException.Pending) throw CAFFParserPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public long getHeaderSize() {
    long ret = CAFFParserPINVOKE.CIFF_getHeaderSize(swigCPtr);
    return ret;
  }

  public long getContentSize() {
    long ret = CAFFParserPINVOKE.CIFF_getContentSize(swigCPtr);
    return ret;
  }

  public long getImageWidth() {
    long ret = CAFFParserPINVOKE.CIFF_getImageWidth(swigCPtr);
    return ret;
  }

  public long getImageHeight() {
    long ret = CAFFParserPINVOKE.CIFF_getImageHeight(swigCPtr);
    return ret;
  }

  public string getCaption() {
    string ret = CAFFParserPINVOKE.CIFF_getCaption(swigCPtr);
    return ret;
  }

  public StringVector getTags() {
    StringVector ret = new StringVector(CAFFParserPINVOKE.CIFF_getTags(swigCPtr), false);
    return ret;
  }

  public BytesVector getPixels() {
    BytesVector ret = new BytesVector(CAFFParserPINVOKE.CIFF_getPixels(swigCPtr), false);
    return ret;
  }

  public bool isValid() {
    bool ret = CAFFParserPINVOKE.CIFF_isValid(swigCPtr);
    return ret;
  }

  public StringVector getParseFails() {
    StringVector ret = new StringVector(CAFFParserPINVOKE.CIFF_getParseFails(swigCPtr), false);
    return ret;
  }

}

}
