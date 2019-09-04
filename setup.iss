; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "BatchPlotPdf"
#define MyAppVersion "1.3.5"
#define MyAppPublisher "XuGuang, Inc."

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{1A7D4226-9B83-4EC5-BAB7-AAB648568325}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=F:\setup
OutputBaseFilename=mysetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\AcDx.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\AcMr.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\BatchPlotPdf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\BatchPlotPdf.ini"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\BatchPlotPdf.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\INIFileParser.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\INIFileParser.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\log4net.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\log4net.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\BatchPlotPdf\bin\Debug\log4net.config"; DestDir: "{app}"; Flags: ignoreversion

Source: "F:\project\BatchPlotPdf\Plot\acad_幕墙.ctb"; DestDir: {code:GetPlotStylesDir}; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\Plot\DWG To PDF.pc3"; DestDir: {code:GetPlotDir}; Flags: ignoreversion
Source: "F:\project\BatchPlotPdf\Plot\DWG To PDF.pmp"; DestDir: {code:GetPlotPmpDir}; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
[Code]

// Global type definitions
type

  AUTOCADINFO = record
    location: AnsiString;
    productname: AnsiString;
    productid: AnsiString;
    serialnumber: AnsiString;
    registrykey: AnsiString;
    version: AnsiString;
    langAbbrev:AnsiString;
    plotDir:AnsiString;
    productnameglob:AnsiString;
  end;
  AcadInfoArray = array of AUTOCADINFO;

// Global variables
var
  SelectAutoCADPage: TInputOptionWizardPage;
  AcadInfos: AcadInfoArray;
  RootKey: Integer;

// Function declarations
function GetInstalledAutoCADVersions(): Boolean; forward;
function GetAutoCADProductName(Param: String): String; forward;

// Function implementations
function InitializeSetup(): Boolean;
var
  bRes: Boolean;
begin;
  bRes := False;
  Log('InitializeSetup() called');
  bRes := GetInstalledAutoCADVersions();
  if bRes = False then begin
    MsgBox('机器上未安装AutoCAD软件。' + #13#10 + '安装将被终止.', mbInformation, MB_OK);
  end;
  Result := bRes;
end;

procedure InitializeWizard;
var
  iCountAcadProductNames, I: Integer;
  button : TButton;
begin
  Log('InitializeWizard() called');

  // AutoCAD version selection dialog
  SelectAutoCADPage := CreateInputOptionPage(wpWelcome, 'AutoCAD 版本选择', '该软件将安装在AutoCAD之上.',
    '选择AutoCAD版本，然后按“下一步”.', True, False);
  iCountAcadProductNames := GetArrayLength(AcadInfos);
  for I := 0 to iCountAcadProductNames - 1 do
  begin
    SelectAutoCADPage.Add(AcadInfos[I].productname);
  end;
end;

function GetPlotDir(Param: String): String;
begin
  Result := AcadInfos[SelectAutoCADPage.SelectedValueIndex].plotDir;
end;
function GetPlotPmpDir(Param: String): String;
begin
  Result := AcadInfos[SelectAutoCADPage.SelectedValueIndex].plotDir + '\' + 'PMP Files';
end;

function GetPlotStylesDir(Param: String): String;
begin
  Result := AcadInfos[SelectAutoCADPage.SelectedValueIndex].plotDir + '\' + 'Plot Styles';
end;

function GetInstalledAutoCADVersions(): Boolean;
var
  S, V, AcadRegKey, sAcadExeLocation, sProductName,sProductNameGlob, sProductId, sSerialNumber, sLangAbbrev,sRegistryKey: String;
  I, J, iCountAcadExeLocations: Integer;
  AcadVerNames, AcadVerKeysTemp: TArrayOfString;
  AcadInfo: AUTOCADINFO;
begin
  if IsWin64 then begin
    RootKey := HKLM64;
  end else begin
    RootKey := HKEY_LOCAL_MACHINE;
  end;

  iCountAcadExeLocations := 0;  
  AcadRegKey := 'SOFTWARE\Autodesk\AutoCAD';
  if RegGetSubkeyNames(RootKey, AcadRegKey, AcadVerNames) then
  begin
    S := '';
    for I := 0 to GetArrayLength(AcadVerNames) - 1 do
    begin
      //MsgBox(AcadRegKey + '\' + AcadVerNames[I], mbInformation, MB_OK);
      if RegGetSubkeyNames(RootKey, AcadRegKey + '\' + AcadVerNames[I], AcadVerKeysTemp) then
      begin
        for J := 0 to GetArrayLength(AcadVerKeysTemp)-1 do
        begin
          //SOFTWARE\Autodesk\AutoCAD\R17.2\ACAD-7000:409
          //MsgBox(AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], mbInformation, MB_OK);
          sAcadExeLocation := '';
          sRegistryKey := AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J];
          if RegQueryStringValue(RootKey, AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], 'Location', sAcadExeLocation) then
          begin
            sProductName := '';
            sProductId := '';
            sSerialNumber := '';
            SetArrayLength(AcadInfos, iCountAcadExeLocations + 1);
            AcadInfo.location := sAcadExeLocation;
            AcadInfo.version := AcadVerNames[I];
            if RegQueryStringValue(RootKey, AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], 'ProductName', sProductName) then begin
              AcadInfo.productname := sProductName;
            end;
            if RegQueryStringValue(RootKey, AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], 'ProductNameGlob', sProductNameGlob) then begin
              AcadInfo.productnameglob := sProductNameGlob;
            end;
            if RegQueryStringValue(RootKey, AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], 'ProductId', sProductId) then begin
              AcadInfo.productid := sProductId;
            end;
            if RegQueryStringValue(RootKey, AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], 'SerialNumber', sSerialNumber) then begin
              AcadInfo.serialnumber := sSerialNumber;
            end;
            if RegQueryStringValue(RootKey, AcadRegKey + '\' + AcadVerNames[I] + '\' + AcadVerKeysTemp[J], 'LangAbbrev', sLangAbbrev) then begin
              AcadInfo.langAbbrev := sLangAbbrev;
            end;
//C:\Users\homeDesignCAD\AppData\Roaming\Autodesk\AutoCAD 2014\R19.1\chs\Plotters
            AcadInfo.registrykey := sRegistryKey;
            AcadInfo.plotDir := 'C:\Users\' + GetUserNameString() + '\AppData\Roaming\Autodesk\' + sProductNameGlob + '\' + AcadVerNames[I] + '\' + sLangAbbrev + '\' + 'Plotters' 
            AcadInfos[iCountAcadExeLocations] := AcadInfo;
            //MsgBox('Location = ' + AcadInfo.location + #13#10 + 'ProductName = ' + AcadInfo.productname + #13#10 + 'ProductId = ' + AcadInfo.productid + #13#10 + 'SerialNumber = ' + AcadInfo.serialnumber + #13#10, mbInformation, MB_OK);
            
            iCountAcadExeLocations := iCountAcadExeLocations + 1;
          end;
        end;
      end;
    end;
    //MsgBox('Founded AutoCAD registry keys:'#13#10#13#10 + S, mbInformation, MB_OK);
  end;
  if iCountAcadExeLocations > 0 then begin
    Result := True;
  end else begin
    Result := False;
  end;
end;

function GetAutoCADProductName(Param: String): String;
var
  sRet: String;
begin
  // AutoCAD 2012
  if Param = 'ACAD-A000:409' then begin
    sRet := 'Autodesk Civil 3D 2012';
  end else if Param = 'ACAD-A001:409' then begin
    sRet := 'AutoCAD 2012';
  end else if Param = 'ACAD-A002:409' then begin
    sRet := 'Autodesk Map 3D 2012';

  // AutoCAD 2013
  end else if Param = 'ACAD-B000:409' then begin
    sRet := 'Autodesk Civil 3D 2013';
  end else if Param = 'ACAD-B001:409' then begin
    sRet := 'AutoCAD 2013';
  end else if Param = 'ACAD-B002:409' then begin
    sRet := 'Autodesk Map 3D 2013';

  // AutoCAD 2009
  end else if Param = 'ACAD-7000:409' then begin
    sRet := 'Autodesk Civil 3D 2009';
  end else if Param = 'ACAD-7001:409' then begin
    sRet := 'AutoCAD 2009';
  end else if Param = 'ACAD-7002:409' then begin
    sRet := 'Autodesk Map 3D 2009';

  // AutoCAD 2010
  end else if Param = 'ACAD-8000:409' then begin
    sRet := 'Autodesk Civil 3D 2010';
  end else if Param = 'ACAD-8001:409' then begin
    sRet := 'AutoCAD 2010';
  end else if Param = 'ACAD-8002:409' then begin
    sRet := 'Autodesk Map 3D 2010';

  // AutoCAD 2011
  end else if Param = 'ACAD-9000:409' then begin
    sRet := 'Autodesk Civil 3D 2011';
  end else if Param = 'ACAD-9001:409' then begin
    sRet := 'AutoCAD 2011';
  end else if Param = 'ACAD-9002:409' then begin
    sRet := 'Autodesk Map 3D 2011';

  // AutoCAD 2014
  end else if Param = 'ACAD-D000:409' then begin
    sRet := 'Autodesk Civil 3D 2014';
  end else if Param = 'ACAD-D001:409' then begin
    sRet := 'AutoCAD 2014';
  end else if Param = 'ACAD-D002:409' then begin
    sRet := 'Autodesk Map 3D 2014';

  // AutoCAD 2015
  end else if Param = 'ACAD-E000:409' then begin
    sRet := 'Autodesk Civil 3D 2015';
  end else if Param = 'ACAD-E001:409' then begin
    sRet := 'AutoCAD 2015';
  end else if Param = 'ACAD-E002:409' then begin
    sRet := 'Autodesk Map 3D 2015';

  // AutoCAD 2016
  end else if Param = 'ACAD-F000:409' then begin
    sRet := 'Autodesk Civil 3D 2016';
  end else if Param = 'ACAD-F001:409' then begin
    sRet := 'AutoCAD 2016';
  end else if Param = 'ACAD-F002:409' then begin
    sRet := 'Autodesk Map 3D 2016';

  // Something other AutoCAD version
  end else begin
    sRet := 'AutoCAD的未知版本: ' + Param;
  end;
  //MsgBox('GetAutoCADProductName(' + Param + ') = ' + sRet, mbInformation, MB_OK);
  Result := sRet;  
end;

function GetAutoCADExe(Param: String): String;
begin
  Result := AcadInfos[SelectAutoCADPage.SelectedValueIndex].location;
end;

function GetTemplateProfileRegKey(Param: String): String;
var
  prodId, regkey: String;
begin
  prodId := AcadInfos[SelectAutoCADPage.SelectedValueIndex].productid;
  regkey := AcadInfos[SelectAutoCADPage.SelectedValueIndex].registrykey;
  Log('CheckAutoCADStartupParameters prodId: ' + prodId);
  if prodId = 'F000' then begin // Civil 3D 2016
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = 'E000' then begin // Civil 3D 2015
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = 'D000' then begin // Civil 3D 2014
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = 'B000' then begin // Civil 3D 2013
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = 'A000' then begin // Civil 3D 2012
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = '9000' then begin // Civil 3D 2011
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = '8000' then begin // Civil 3D 2010
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else if prodId = '7000' then begin // Civil 3D 2009
    Result := regkey + '\Profiles\<<C3D_Metric>>';
  end else begin
    Result := regkey + '\Profiles\<<Unnamed Profile>>';
  end;
end;

function CheckAutoCADStartupParameters(Param: String): String;
var
  prodId, path: String;
begin
  prodId := AcadInfos[SelectAutoCADPage.SelectedValueIndex].productid;
  path := AcadInfos[SelectAutoCADPage.SelectedValueIndex].location;
  Log('CheckAutoCADStartupParameters prodId: ' + prodId);
  Log('CheckAutoCADStartupParameters location: ' + path);
  if prodId = 'F000' then begin // Civil 3D 2016
    Result := '/ld "' + path + '\AecBase.dbx" /product "C3D" ';
  end else if prodId = 'F002' then begin // Map 3D 2016
    Result := '/product MAP /language "en-US" ';
  end else if prodId = 'E000' then begin // Civil 3D 2015
    Result := '/ld "' + path + '\AecBase.dbx" /product "C3D" ';
  end else if prodId = 'D000' then begin // Civil 3D 2014
    Result := '/ld "' + path + '\AecBase.dbx" ';
  end else if prodId = 'B000' then begin // Civil 3D 2013
    Result := '/ld "' + path + '\AecBase.dbx" ';
  end else if prodId = 'A000' then begin // Civil 3D 2012
    Result := '/ld "' + path + '\AecBase.dbx" ';
  end else if prodId = '9000' then begin // Civil 3D 2011
    Result := '/ld "' + path + '\AecBase.dbx" ';
  end else if prodId = '8000' then begin // Civil 3D 2010
    Result := '/ld "' + path + '\AecBase.dbx" ';
  end else if prodId = '7000' then begin // Civil 3D 2009
    Result := '/ld "' + path + '\AecBase.dbx" ';
  end else begin
    Result := '';
  end;
end;