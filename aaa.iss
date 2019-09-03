; 
; Demonstrates setup of demand loading code for LspLoad application - thanks to Owen Wengerd
; Download LspLoad.zip from: http://www.manusoft.com/software/freebies/arx.html
; Download Inno Setup: http://www.jrsoftware.org/isinfo.php/
; 

[Setup]
AppName=MyApp
AppVersion=1.0
VersionInfoVersion=1.0
DefaultDirName={pf}\MyApp
DefaultGroupName=MyApp
DisableProgramGroupPage=true
AlwaysUsePersonalGroup=true
PrivilegesRequired=none
ArchitecturesInstallIn64BitMode=x64

WindowShowCaption=true

Uninstallable=yes

[Messages]
DiskSpaceMBLabel=The program requires at least [kb] KB of disk space.

[Dirs]
Name: {app}; Permissions: users-modify

[Files]
Source: MyApp.lsp; DestDir: {app}; Flags: touch

;; include OpenDCL runtime for your app if required
;; Source: OpenDCL.Runtime.7.0.0.13.msi; DestDir: {app}; Flags: ignoreversion

;; LspLoad file is renamed when copied to the installation directory to match the application name
;; Boolean functions GetVer and Is64Bit check appropriate file to install for the installation

Source: LspLoad.9.brx; DestDir: {app}; DestName: MyApp.9.brx; Check: GetVer(9)
Source: LspLoad.10.brx; DestDir: {app}; DestName: MyApp.10.brx; Check: GetVer(10)
Source: LspLoad.11.brx; DestDir: {app}; DestName: MyApp.11.brx; Check: GetVer(11)
Source: LspLoad.12.brx; DestDir: {app}; DestName: MyApp.12.brx; Check: GetVer(12)
Source: LspLoad.15.arx; DestDir: {app}; DestName: MyApp.15.arx; Check: GetVer(15)
Source: LspLoad.16.arx; DestDir: {app}; DestName: MyApp.16.arx; Check: GetVer(16)
Source: LspLoad.17.arx; DestDir: {app}; DestName: MyApp.17.arx; Check: not Is64Bit and GetVer(17)
Source: LspLoad.17.x64.arx; DestDir: {app}; DestName: MyApp.17.x64.arx; Check: Is64Bit and GetVer(17)
Source: LspLoad.18.arx; DestDir: {app}; DestName: MyApp.18.arx; Check: not Is64Bit and GetVer(18)
Source: LspLoad.18.x64.arx; DestDir: {app}; DestName: MyApp.18.x64.arx; Check: Is64Bit and GetVer(18)
Source: LspLoad.19.arx; DestDir: {app}; DestName: MyApp.19.arx; Check: not Is64Bit and GetVer(19)
Source: LspLoad.19.x64.arx; DestDir: {app}; DestName: MyApp.19.x64.arx; Check: Is64Bit and GetVer(19)

[Icons]
Name: {group}\Uninstall MyApp; Filename: {uninstallexe}; WorkingDir: {app}; 

[INI]

[InstallDelete]

[Registry]
;; Include path of installation in registry if required
;; Root: HKCU; Subkey: Software\MyAppCo\MyApp; ValueType: string; ValueName: InstallPath; ValueData: {app}; Flags: uninsdeletekey;

;; Insert demand loader entry for LspLoad.xx.arx
;; GetFilenm function retreives file name string
;; GetAcadAppReg gets registry path for HKCU  

;; For HKLM (all users) you will need to Create a page in the setup wizard to select setup option - 
;; either everyone or current user and create a function similar to GetAcadAppReg  

;; Set LoadCtrls key to 12 for demand loading using command specified in Commands key (TestMyApp)
;; Set LoadCtrls key to 2 for load at AutoCAD startup

Root: HKCU; Subkey: {code:GetAcadAppReg}\Applications\MyApp; ValueType: string; ValueName: Description; ValueData: MyApp; Flags: uninsdeletekey; 
Root: HKCU; Subkey: {code:GetAcadAppReg}\Applications\MyApp; ValueType: dword; ValueName: LoadCtrls; ValueData: 12; Flags: uninsdeletekey; 
Root: HKCU; Subkey: {code:GetAcadAppReg}\Applications\MyApp; ValueType: string; ValueName: Loader; ValueData: {app}\{code:GetFilenm}; Flags: uninsdeletekey; 
Root: HKCU; Subkey: {code:GetAcadAppReg}\Applications\MyApp\Commands; ValueType: string; ValueName: MyApp; ValueData: TestMyApp;

;;include OpenDCL runtime for your app if required
[Run] 
;; Filename: msiexec.exe; Parameters: "/i ""{app}\OpenDCL.Runtime.7.0.0.13.msi"""

[UninstallRun]
;; Filename: msiexec.exe; Parameters: "/x ""{app}\OpenDCL.Runtime.7.0.0.13.msi"""

[Code]
// The following InitializeSetup function is a basic check for installation of AutoCAD or Bricscad. 
// It doesn't allow for a choice if there are multiple installations on the same system and hasn't been tested extensively  
// so may not work for products other than vanilla AutoCAD 

var
App, Version, Product, Ver, Wow, AcadLocn: String;
       
function InitializeSetup (): Boolean;
  begin //check for apps including AutoCAD, AutoCAD LT, Bricscad
     if RegKeyExists(HKEY_CLASSES_ROOT, 'AutoCAD.Application') or RegKeyExists(HKEY_CLASSES_ROOT, 'AutoCADLT.Drawing') then 
       begin
          RegQueryStringValue(HKEY_CLASSES_ROOT, 'AutoCAD.Application\CurVer','', Version)
          if (Version = '') or RegKeyExists(HKEY_CLASSES_ROOT, 'AutoCADLT.Drawing\CurVer') then
             begin
               if RegValueExists(HKEY_CURRENT_USER, 'Software\Autodesk\AutoCAD LT','CurVer') then
               MsgBox('AutoCAD LT is not supported, exiting install...', mbInformation, MB_OK) else
               MsgBox('Can''t locate AutoCAD or Bricscad on this system, exiting install...', mbInformation, MB_OK)
               Result := False;
             end
             else
                if Version = 'BricscadApp.AcadApplication' then
                   begin      
                      App := 'Bricscad'
                      RegQueryStringValue(HKEY_CURRENT_USER, 'Software\Bricsys\Bricscad','CURVER', Version)//Reassign variable Version
                      RegQueryStringValue(HKEY_CURRENT_USER, ('Software\Bricsys\Bricscad\' + Version),'CURVER', Product)//Assign the variable Product
                      RegQueryStringValue(HKEY_LOCAL_MACHINE, ('Software\Bricsys\Bricscad\' + Version + '\' + Product),'AcadLocation', AcadLocn)//AcadLocn
                      Wow := '0' //Flag
                      Ver := Version 
                      Delete (Ver, 1, 1)//remove leading V 
                      Result := True;
                   end
                   else
                     if Version = 'AutoCAD.Drawing.15' then
                        begin
                           App := 'AutoCAD'
                           RegQueryStringValue(HKEY_LOCAL_MACHINE, 'Software\Autodesk\AutoCAD','CurVer', Version)
                           RegQueryStringValue(HKEY_LOCAL_MACHINE, ('Software\Autodesk\AutoCAD\' + Version),'CurVer', Product)
                           //Create a registry string as global 'Wow' to check later
                           if RegValueExists(HKEY_LOCAL_MACHINE, ('Software\Autodesk\AutoCAD\' + Version + '\' + Product), 'AcadLocation') then
                                    begin 
                                      RegQueryStringValue(HKEY_LOCAL_MACHINE, ('SOFTWARE\Autodesk\AutoCAD\' + Version + '\' + Product),'AcadLocation', AcadLocn)
                                      Wow := '0'
                                    end
                                    else
                                      if RegValueExists(HKEY_LOCAL_MACHINE, ('SOFTWARE\Wow6432Node\Autodesk\AutoCAD\' + Version + '\' + Product), 'AcadLocation') then
                                        begin 
                                           RegQueryStringValue(HKEY_LOCAL_MACHINE, ('SOFTWARE\Wow6432Node\Autodesk\AutoCAD\' + Version + '\' + Product),'AcadLocation', AcadLocn)
                                           Wow := '1'
                                        end 
                           Ver := Version //create global 
                           Delete (Ver, 4, 2)//remove trailing period and integer
                           Delete (Ver, 1, 1)//remove leading R
                           Result := True;
                        end
                        else             
                           if RegValueExists(HKEY_CURRENT_USER, 'Software\Autodesk\AutoCAD','CurVer') then
                              begin
                                 App := 'AutoCAD'
                                 RegQueryStringValue(HKEY_CURRENT_USER, 'Software\Autodesk\AutoCAD','CurVer', Version)//get most recent accessed version regardless of installations
                                 RegQueryStringValue(HKEY_CURRENT_USER, ('Software\Autodesk\AutoCAD\' + Version),'CurVer', Product)
                                //Check where registry entries are found to determine whether installation is 32 bit or 64 bit
                                 if RegValueExists(HKEY_LOCAL_MACHINE, ('Software\Autodesk\AutoCAD\' + Version + '\' + Product), 'AcadLocation') then
                                    begin 
                                      RegQueryStringValue(HKEY_LOCAL_MACHINE, ('SOFTWARE\Autodesk\AutoCAD\' + Version + '\' + Product),'AcadLocation', AcadLocn)
                                      Wow := '0'
                                    end
                                    else
                                      if RegValueExists(HKEY_LOCAL_MACHINE, ('SOFTWARE\Wow6432Node\Autodesk\AutoCAD\' + Version + '\' + Product), 'AcadLocation') then
                                        begin 
                                           RegQueryStringValue(HKEY_LOCAL_MACHINE, ('SOFTWARE\Wow6432Node\Autodesk\AutoCAD\' + Version + '\' + Product),'AcadLocation', AcadLocn)
                                           Wow := '1'
                                        end 
                                 Ver := Version //create global 
                                 Delete (Ver, 4, 2)//remove trailing period and integer
                                 Delete (Ver, 1, 1)//remove leading R
                                 Result := True; //flag install to continue
                              end
                              else
                                begin
                                   MsgBox('Can''t locate AutoCAD or Bricscad on this system, exiting install...', mbInformation, MB_OK) 
                                   Result := False;
                                end
       end
       else
         begin
           MsgBox('Can''t locate AutoCAD or Bricscad on this system, exiting install...', mbInformation, MB_OK) 
           Result := False;
         end
  end;


function GetFileNm(FileNm: String): String;
  begin
    if 'AutoCAD' = App then
      begin
        case Ver of
          '15' : FileNm := 'MyApp.15.arx';
          '16' : FileNm := 'MyApp.16.arx';
          //check if the 'Wow' flag is set to '1' if so the installation is a 32bit version on a 64bit OS
          '17' : if Wow = '1' then
                   FileNm := 'MyApp.17.arx' else
                   //installation is under a 64bit or 32bit OS
                   if isWin64 then 
                   FileNm := 'MyApp.17.x64.arx' else
                   FileNm := 'MyApp.17.arx';
          '18' : if Wow = '1' then
                   FileNm := 'MyApp.18.arx' else
                   if isWin64 then 
                   FileNm := 'MyApp.18.x64.arx' else
                   FileNm := 'MyApp.18.arx';
          '19' :  if Wow = '1' then
                   FileNm := 'MyApp.19.arx' else
                   if isWin64 then 
                   FileNm := 'MyApp.19.x64.arx' else
                   FileNm := 'MyApp.19.arx';
        end;
          Result := FileNm
      end
    else
      begin
          //Bricscad ...
          case Ver of
            '12' : FileNm := 'MyApp.12.brx';
            '11' : FileNm := 'MyApp.11.brx';
            '10' : FileNm := 'MyApp.10.brx';
            '9' : FileNm := 'MyApp.9.brx';
          end;
         Result := FileNm
      end
  end;

function GetAcadAppReg (RegPath: String): String;
  begin
    if 'AutoCAD' = App then
      begin
        RegPath := ('Software\Autodesk\AutoCAD\' + Version + '\' + Product)
        Result := RegPath
      end 
      else
      begin
        //Must be Bricscad
        RegPath := ('Software\Bricsys\Bricscad\' + Version + '\' + Product)
        Result := RegPath
      end
  end;
 
function GetVer (Param: Integer): Boolean;
  begin
     if Param = StrToInt(Ver) then
        Result := True  else 
        Result := False
  end;

function Is64Bit (): Boolean;
   begin
      if Wow = '1' then
         Result := False else
      if isWin64 then
         Result := True else
         Result := False
   end;

