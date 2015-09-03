RELEASE NOTES
=============

What's new in 1.0.0-alpha3 (_03/Sep/2015_)
------------------------------------------


### **Improvements**
  - Add help comments to the Areas & Iterations functions

### **Bug fixes**
  - Fix an issue in the AssemblyResolver implementation. Previously it was implemented as a scriptblock and was running into some race conditions that would crash PowerShell. Switched to a pure .NET implementation in order to avoid the race condition.
