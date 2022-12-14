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

public class CIFFAndDuration : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CIFFAndDuration(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CIFFAndDuration obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(CIFFAndDuration obj) {
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

  ~CIFFAndDuration() {
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
          CAFFParserPINVOKE.delete_CIFFAndDuration(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public CIFFAndDuration(long first, CIFF second) : this(CAFFParserPINVOKE.new_CIFFAndDuration__SWIG_0(first, CIFF.getCPtr(second)), true) {
    if (CAFFParserPINVOKE.SWIGPendingException.Pending) throw CAFFParserPINVOKE.SWIGPendingException.Retrieve();
  }

  public CIFFAndDuration(CIFFAndDuration other) : this(CAFFParserPINVOKE.new_CIFFAndDuration__SWIG_1(CIFFAndDuration.getCPtr(other)), true) {
    if (CAFFParserPINVOKE.SWIGPendingException.Pending) throw CAFFParserPINVOKE.SWIGPendingException.Retrieve();
  }

  public long first {
    set {
      CAFFParserPINVOKE.CIFFAndDuration_first_set(swigCPtr, value);
    } 
    get {
      long ret = CAFFParserPINVOKE.CIFFAndDuration_first_get(swigCPtr);
      return ret;
    } 
  }

  public CIFF second {
    set {
      CAFFParserPINVOKE.CIFFAndDuration_second_set(swigCPtr, CIFF.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = CAFFParserPINVOKE.CIFFAndDuration_second_get(swigCPtr);
      CIFF ret = (cPtr == global::System.IntPtr.Zero) ? null : new CIFF(cPtr, false);
      return ret;
    } 
  }

}

}
