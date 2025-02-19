var printer_profiles = [
{
  "LINEPRINTERCONTROL":
  {
    "FormatVersion": "1.0.0.0",
    "Platform": "Android",
    "DEFAULTS":
    {
      "BoldIsFont": true, "BoldOff": "[0x1b,0x77,!]", "BoldOn": "[0x1b,0x77,0x6D]",
      "CompressDotsHigh": 12, "CompressIsFont": true, "CompressOff": "[0x1b,0x77,!]",
      "CompressOn": "[0x1b,0x77,0x42]", "DoubleHighDotsHigh": 32, "DoubleHighIsFont": false,
      "DoubleHighMask": 16, "DoubleHighOff": "[0x1b,!,0x10]", "DoubleHighOn": "[0x1b,!,0x10]",
      "DoubleWideDotsHigh": 16, "DoubleWideHighPrefix": "[0x1b,!]", "DoubleWideIsFont": false,
      "DoubleWideMask": 32, "DoubleWideOff": "[0x1b,!,0x20]", "DoubleWideOn": "[0x1b,!,0x20]",
      "Initialize": "[0x00,0x00,0x00,0x00,0x1b,0x40,0x00,0x00,0x00]",
      "NormalFont": "[0x1b,0x77,!,0x1b,!,0x00]", "NewLine": "[0x0d]", "NormalDotsHigh": 16,
      "FormFeed": "[0x0d,0x0d,0x0d,0x0d,0x0d]", "ValidateAttribOnFormFeed": true,
      "PrintHeadWidth": 384, "UseDoubleHWMask": true, "ValidateAttribOnFont": true,
      "ValidateAttribOnNewLn": true, "NullsBeforeClose": 0, "PreCloseDelay": 0, "EndOfGraphicsDelay": 0,
      "PostGraphicsDelay": 0, "PostGraphicsLineDelay": 0, "PreGraphicsDelay": 0, "StartOfGraphicsDelay": 0,
      "BtConnectRetries" : 3, "BtconnectRetryDelay" : 100, "BtWriteDataReadyTimeout" : 10000, "BtWriteIntervalTimeout" : 10000,
      "BtMaxSegWrite" : 1024, "BtMaxSegRead" : 1024, "BtLinger" : 10
    },

    "PRINTERS":
    {
      "P6824":
      {
        "DisplayName": "6824 Printer",
        "PrintHeadWidth": 960,
        "Initialize": "[0x1b,0x40,0x0d,0x00,0x00,0x00,0x00,0x00]",
        "NormalDotsHigh": 16, "NormalFont": "[0x1b,!,0x00]", "NewLine": "[0x0d,0x0a]",
        "BoldIsFont": false, "BoldOff": "[0x1b,H]", "BoldOn": "[0x1b,G]",
        "CompressIsFont": false, "CompressDotsHigh": 12, "CompressOff": "[0x12]", "CompressOn": "[0x1b,0x0f]",
        "DoubleWideIsFont": false, "DoubleWideDotsHigh": 12, "DoubleWideOff": "[0x1b,0x57,0x00]",
        "DoubleWideOn": "[0x1b,0x57,0x01]", "FormFeed": "[0x0c]",
        "ItalicIsFont": false, "ItalicOn": "[0x1b,0x34]", "ItalicOff": "[0x1b,0x35]",
        "UnderlineIsFont": false, "UnderlineOn": "[0x1b,0x2d,0x01]", "UnderlineOff": "[0x1b,0x2d,0x00]",
        "DoubleHighDotsHigh": 12, "DoubleHighIsFont": false, "DoubleHighOff": "[0x00]", "DoubleHighOn": "[0x00]",
        "UseDoubleHWMask": false, "PreCloseDelay": 6000
      },

      "PR2":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR2 Bt Printer",  "PrintHeadWidth": 384,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PR3":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR3 Bt Printer",  "PrintHeadWidth": 576,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB21":
      {
        "DisplayName":"PB21 Bt Printer", "PrintHeadWidth": 384, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB31":
      {
        "DisplayName":"PB31 Bt Printer", "PrintHeadWidth": 576, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB42":
      {
        "DisplayName":"PB42 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 4500,
        "NullsBeforeClose": 4000, "StartOfGraphicsDelay": 600, "INCLUDE_02": "BARCODE_SETTINGS",
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB51":
      {
        "DisplayName":"PB51 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 500,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB22_Fingerprint":
      {
        "DisplayName":"PB22 Bt Label Printer", "PrintHeadWidth": 384, "LABEL_01": "2in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PB32_Fingerprint":
      {
        "DisplayName":"PB32 Bt Label Printer", "PrintHeadWidth": 576, "LABEL_01": "3in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PR_SETTINGS":
      {
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10, "PreCloseDelay": 1000, "NullsBeforeClose": 8000
      },

      "BARCODE_SETTINGS":
      {
        "Code39NarrowWidth": 2, "Code39WideWidth": 4, "Code128NarrowWidth": 2
      }
    },

    "LABELS":
    {
      "2in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 30, 120:FT \"Swiss 721 Bold Condensed BT\",16:PT \"ItemName$$\":PP 120,75:BARSET \"CODE128\",3,1,4,150:PB \"ItemNo$$\":PP 280, 260:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 100, 20:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 200, 20:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 70,450:BARSET \"QRCODE\",1,1,8,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      },

      "3in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 40, 180:FT \"Swiss 721 Bold Condensed BT\",20:PT \"ItemName$$\":PP 150,180:BARSET \"CODE128\",3,1,4,240:PB \"ItemNo$$\":PP 400, 330:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 200, 50:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 300, 50:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 105,490:BARSET \"QRCODE\",1,1,13,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      }
    },
    "STATUS":
    {
      "ESCP_StatusQueries" :
      {
        "QUERIES" :
        [
          {
            "GetStatusCommand" : "[0x1b,{,S,T,?,}]",
            "StatusInitTimeout" : 5000,
            "StatusGapTimeout" : 500,
            "StatusEndResponse" : "[}]",
            "RESPONSES" :
            [
              { "PrinterMsg" : "[P,:,N]", "MsgNo" : 223 },
              { "PrinterMsg" : "[B,:,V]", "MsgNo" : 226 },
              { "PrinterMsg" : "[L,:,U]", "MsgNo" : 227 },
              { "PrinterMsg" : "[H,:,T]", "MsgNo" : 228 },
              { "PrinterMsg" : "[E,:,c]", "MsgNo" : 1001 },
              { "PrinterMsg" : "[E,:,d]", "MsgNo" : 1002 },
              { "PrinterMsg" : "[E,:,f]", "MsgNo" : 1003 },
              { "PrinterMsg" : "[E,:,g]", "MsgNo" : 1004 }
            ]
          }
        ]
      }
    }
  }
}
,
{
  "LINEPRINTERCONTROL":
  {
    "FormatVersion": "1.0.0.0",
    "Platform": "iOS",
    "DEFAULTS":
    {
      "BoldIsFont": true, "BoldOff": "[0x1b,0x77,!]", "BoldOn": "[0x1b,0x77,0x6D]",
      "CompressDotsHigh": 12, "CompressIsFont": true, "CompressOff": "[0x1b,0x77,!]",
      "CompressOn": "[0x1b,0x77,0x42]", "DoubleHighDotsHigh": 32, "DoubleHighIsFont": false,
      "DoubleHighMask": 16, "DoubleHighOff": "[0x1b,!,0x10]", "DoubleHighOn": "[0x1b,!,0x10]",
      "DoubleWideDotsHigh": 16, "DoubleWideHighPrefix": "[0x1b,!]", "DoubleWideIsFont": false,
      "DoubleWideMask": 32, "DoubleWideOff": "[0x1b,!,0x20]", "DoubleWideOn": "[0x1b,!,0x20]",
      "Initialize": "[0x00,0x00,0x00,0x00,0x1b,0x40,0x00,0x00,0x00]",
      "NormalFont": "[0x1b,0x77,!,0x1b,!,0x00]", "NewLine": "[0x0d]", "NormalDotsHigh": 16,
      "FormFeed": "[0x0d,0x0d,0x0d,0x0d,0x0d]", "ValidateAttribOnFormFeed": true,
      "PrintHeadWidth": 384, "UseDoubleHWMask": true, "ValidateAttribOnFont": true,
      "ValidateAttribOnNewLn": true, "NullsBeforeClose": 0, "PreCloseDelay": 0, "EndOfGraphicsDelay": 0, 
      "PostGraphicsDelay": 0, "PostGraphicsLineDelay": 0, "PreGraphicsDelay": 0, "StartOfGraphicsDelay": 0,
      "BtConnectRetries" : 3, "BtconnectRetryDelay" : 100, "BtWriteDataReadyTimeout" : 10000, "BtWriteIntervalTimeout" : 10000, 
      "BtMaxSegWrite" : 1024, "BtMaxSegRead" : 1024, "BtLinger" : 10
    },

    "PRINTERS":
    {
      "P6824":
      {
        "DisplayName": "6824 Printer",
        "PrintHeadWidth": 960,
        "Initialize": "[0x1b,0x40,0x0d,0x00,0x00,0x00,0x00,0x00]",
        "NormalDotsHigh": 16, "NormalFont": "[0x1b,!,0x00]",
        "BoldIsFont": false, "BoldOff": "[0x1b,H]", "BoldOn": "[0x1b,G]",
        "CompressIsFont": false, "CompressDotsHigh": 12, "CompressOff": "[0x1b,0x35]", "CompressOn": "[0x1b,0x34]",
        "DoubleWideIsFont": false, "DoubleWideDotsHigh": 12, "DoubleWideOff": "[0x1b,0x0e]", "DoubleWideOn": "[0x1b,0x14]",
        "FormFeed": "[0x0c]",
        "ItalicIsFont": false, "ItalicOn": "[0x1b,0x34]", "ItalicOff": "[0x1b,0x35]",
        "UnderlineIsFont": false, "UnderlineOn": "[0x1b,0x2d,0x01]", "UnderlineOff": "[0x1b,0x2d,0x00]"
      },

      "PR2":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR2 Bt Printer",  "PrintHeadWidth": 384,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PR3":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR3 Bt Printer",  "PrintHeadWidth": 576,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB21":
      {
        "DisplayName":"PB21 Bt Printer", "PrintHeadWidth": 384, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB31":
      {
        "DisplayName":"PB31 Bt Printer", "PrintHeadWidth": 576, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB42":
      {
        "DisplayName":"PB42 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 4500,
        "NullsBeforeClose": 4000, "StartOfGraphicsDelay": 600, "INCLUDE_02": "BARCODE_SETTINGS",
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB51":
      {
        "DisplayName":"PB51 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 500,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB22_Fingerprint":
      {
        "DisplayName":"PB22 Bt Label Printer", "PrintHeadWidth": 384, "LABEL_01": "2in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PB32_Fingerprint":
      {
        "DisplayName":"PB32 Bt Label Printer", "PrintHeadWidth": 576, "LABEL_01": "3in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PR_SETTINGS":
      {
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10, "PreCloseDelay": 1000, "NullsBeforeClose": 8000
      },

      "BARCODE_SETTINGS":
      {
        "Code39NarrowWidth": 2, "Code39WideWidth": 4, "Code128NarrowWidth": 2
      }
    },

    "LABELS":
    {
      "2in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 30, 120:FT \"Swiss 721 Bold Condensed BT\",16:PT \"ItemName$$\":PP 120,75:BARSET \"CODE128\",3,1,4,150:PB \"ItemNo$$\":PP 280, 260:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 100, 20:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 200, 20:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 70,450:BARSET \"QRCODE\",1,1,8,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      },

      "3in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 40, 180:FT \"Swiss 721 Bold Condensed BT\",20:PT \"ItemName$$\":PP 150,180:BARSET \"CODE128\",3,1,4,240:PB \"ItemNo$$\":PP 400, 330:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 200, 50:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 300, 50:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 105,490:BARSET \"QRCODE\",1,1,13,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      }
    },
    "STATUS":
    {
      "ESCP_StatusQueries" :
      {
        "QUERIES" :
        [
          {
            "GetStatusCommand" : "[0x1b,{,S,T,?,}]",
            "StatusInitTimeout" : 5000,
            "StatusGapTimeout" : 500,
            "StatusEndResponse" : "[}]",
            "RESPONSES" :
            [
              { "PrinterMsg" : "[P,:,N]", "MsgNo" : 223 },
              { "PrinterMsg" : "[B,:,V]", "MsgNo" : 226 },
              { "PrinterMsg" : "[L,:,U]", "MsgNo" : 227 },
              { "PrinterMsg" : "[H,:,T]", "MsgNo" : 228 },
              { "PrinterMsg" : "[E,:,c]", "MsgNo" : 1001 },
              { "PrinterMsg" : "[E,:,d]", "MsgNo" : 1002 },
              { "PrinterMsg" : "[E,:,f]", "MsgNo" : 1003 },
              { "PrinterMsg" : "[E,:,g]", "MsgNo" : 1004 }
            ]
          }
        ]
      }
    }
  }
}
,
{
  "LINEPRINTERCONTROL":
  {
    "FormatVersion": "1.0.0.0",
    "Platform": "Windows",
    "DEFAULTS":
    {
      "BoldIsFont": true, "BoldOff": "[0x1b,0x77,!]", "BoldOn": "[0x1b,0x77,0x6D]",
      "CompressDotsHigh": 12, "CompressIsFont": true, "CompressOff": "[0x1b,0x77,!]",
      "CompressOn": "[0x1b,0x77,0x42]", "DoubleHighDotsHigh": 32, "DoubleHighIsFont": false,
      "DoubleHighMask": 16, "DoubleHighOff": "[0x1b,!,0x10]", "DoubleHighOn": "[0x1b,!,0x10]",
      "DoubleWideDotsHigh": 16, "DoubleWideHighPrefix": "[0x1b,!]", "DoubleWideIsFont": false,
      "DoubleWideMask": 32, "DoubleWideOff": "[0x1b,!,0x20]", "DoubleWideOn": "[0x1b,!,0x20]",
      "Initialize": "[0x00,0x00,0x00,0x00,0x1b,0x40,0x00,0x00,0x00]",
      "NormalFont": "[0x1b,0x77,!,0x1b,!,0x00]", "NewLine": "[0x0d]", "NormalDotsHigh": 16,
      "FormFeed": "[0x0d,0x0d,0x0d,0x0d,0x0d]", "ValidateAttribOnFormFeed": true,
      "PrintHeadWidth": 384, "UseDoubleHWMask": true, "ValidateAttribOnFont": true,
      "ValidateAttribOnNewLn": true, "NullsBeforeClose": 0, "PreCloseDelay": 0, "EndOfGraphicsDelay": 0, 
      "PostGraphicsDelay": 0, "PostGraphicsLineDelay": 0, "PreGraphicsDelay": 0, "StartOfGraphicsDelay": 0,
      "BtConnectRetries" : 3, "BtconnectRetryDelay" : 250, "BtWriteDataReadyTimeout" : 20000, "BtWriteIntervalTimeout" : 20000, 
      "BtMaxSegWrite" : 1024, "BtMaxSegRead" : 1024, "BtLinger" : 20
    },

    "PRINTERS":
    {
      "P6824":
      {
        "DisplayName": "6824 Printer",
        "PrintHeadWidth": 960,
        "Initialize": "[0x1b,0x40,0x0d,0x00,0x00,0x00,0x00,0x00]",
        "NormalDotsHigh": 16, "NormalFont": "[0x1b,!,0x00]",
        "BoldIsFont": false, "BoldOff": "[0x1b,H]", "BoldOn": "[0x1b,G]",
        "CompressIsFont": false, "CompressDotsHigh": 12, "CompressOff": "[0x1b,0x35]", "CompressOn": "[0x1b,0x34]",
        "DoubleWideIsFont": false, "DoubleWideDotsHigh": 12, "DoubleWideOff": "[0x1b,0x0e]", "DoubleWideOn": "[0x1b,0x14]",
        "FormFeed": "[0x0c]",
        "ItalicIsFont": false, "ItalicOn": "[0x1b,0x34]", "ItalicOff": "[0x1b,0x35]",
        "UnderlineIsFont": false, "UnderlineOn": "[0x1b,0x2d,0x01]", "UnderlineOff": "[0x1b,0x2d,0x00]"
      },

      "PR2":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR2 Bt Printer",  "PrintHeadWidth": 384,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PR3":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR3 Bt Printer",  "PrintHeadWidth": 576,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB21":
      {
        "DisplayName":"PB21 Bt Printer", "PrintHeadWidth": 384, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 100, "PostGraphicsLineDelay": 1,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB31":
      {
        "DisplayName":"PB31 Bt Printer", "PrintHeadWidth": 576, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 100, "PostGraphicsLineDelay": 1,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB42":
      {
        "DisplayName":"PB42 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 1000,
        "BtConnectRetries" : 4,"INCLUDE_02":"BARCODE_SETTINGS",
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB51":
      {
        "DisplayName":"PB51 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 500,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB22_Fingerprint":
      {
        "DisplayName":"PB22 Bt Label Printer", "PrintHeadWidth": 384, "LABEL_01": "2in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PB32_Fingerprint":
      {
        "DisplayName":"PB32 Bt Label Printer", "PrintHeadWidth": 576, "LABEL_01": "3in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PR_SETTINGS":
      {
        "PostGraphicsDelay": 100, "PostGraphicsLineDelay": 1, "PreCloseDelay": 1000
      },

      "BARCODE_SETTINGS":
      {
        "Code39NarrowWidth": 2, "Code39WideWidth": 4, "Code128NarrowWidth": 2
      }
    },

    "LABELS":
    {
      "2in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 30, 120:FT \"Swiss 721 Bold Condensed BT\",16:PT \"ItemName$$\":PP 120,75:BARSET \"CODE128\",3,1,4,150:PB \"ItemNo$$\":PP 280, 260:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 100, 20:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 200, 20:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 70,450:BARSET \"QRCODE\",1,1,8,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      },

      "3in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 40, 180:FT \"Swiss 721 Bold Condensed BT\",20:PT \"ItemName$$\":PP 150,180:BARSET \"CODE128\",3,1,4,240:PB \"ItemNo$$\":PP 400, 330:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 200, 50:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 300, 50:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 105,490:BARSET \"QRCODE\",1,1,13,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      }
    },
    "STATUS":
    {
      "ESCP_StatusQueries" :
      {
        "QUERIES" :
        [
          {
            "GetStatusCommand" : "[0x1b,{,S,T,?,}]",
            "StatusInitTimeout" : 5000,
            "StatusGapTimeout" : 500,
            "StatusEndResponse" : "[}]",
            "RESPONSES" :
            [
              { "PrinterMsg" : "[P,:,N]", "MsgNo" : 223 },
              { "PrinterMsg" : "[B,:,V]", "MsgNo" : 226 },
              { "PrinterMsg" : "[L,:,U]", "MsgNo" : 227 },
              { "PrinterMsg" : "[H,:,T]", "MsgNo" : 228 },
              { "PrinterMsg" : "[E,:,c]", "MsgNo" : 1001 },
              { "PrinterMsg" : "[E,:,d]", "MsgNo" : 1002 },
              { "PrinterMsg" : "[E,:,f]", "MsgNo" : 1003 },
              { "PrinterMsg" : "[E,:,g]", "MsgNo" : 1004 }
            ]
          }
        ]
      }
    }
  }
}
,
{
  "LINEPRINTERCONTROL":
  {
    "FormatVersion": "1.0.0.0",
    "Platform": "Windows_CE",
    "DEFAULTS":
    {
      "BoldIsFont": true, "BoldOff": "[0x1b,0x77,!]", "BoldOn": "[0x1b,0x77,0x6D]",
      "CompressDotsHigh": 12, "CompressIsFont": true, "CompressOff": "[0x1b,0x77,!]",
      "CompressOn": "[0x1b,0x77,0x42]", "DoubleHighDotsHigh": 32, "DoubleHighIsFont": false,
      "DoubleHighMask": 16, "DoubleHighOff": "[0x1b,!,0x10]", "DoubleHighOn": "[0x1b,!,0x10]",
      "DoubleWideDotsHigh": 16, "DoubleWideHighPrefix": "[0x1b,!]", "DoubleWideIsFont": false,
      "DoubleWideMask": 32, "DoubleWideOff": "[0x1b,!,0x20]", "DoubleWideOn": "[0x1b,!,0x20]",
      "Initialize": "[0x00,0x00,0x00,0x00,0x1b,0x40,0x00,0x00,0x00]",
      "NormalFont": "[0x1b,0x77,!,0x1b,!,0x00]", "NewLine": "[0x0d]", "NormalDotsHigh": 16,
      "FormFeed": "[0x0d,0x0d,0x0d,0x0d,0x0d]", "ValidateAttribOnFormFeed": true,
      "PrintHeadWidth": 384, "UseDoubleHWMask": true, "ValidateAttribOnFont": true,
      "ValidateAttribOnNewLn": true, "NullsBeforeClose": 0, "PreCloseDelay": 0, "EndOfGraphicsDelay": 0, 
      "PostGraphicsDelay": 0, "PostGraphicsLineDelay": 0, "PreGraphicsDelay": 0, "StartOfGraphicsDelay": 0,
      "BtConnectRetries" : 3, "BtconnectRetryDelay" : 250, "BtWriteDataReadyTimeout" : 20000, "BtWriteIntervalTimeout" : 20000, 
      "BtMaxSegWrite" : 1024, "BtMaxSegRead" : 1024, "BtLinger" : 20
    },

    "PRINTERS":
    {
      "P6824":
      {
        "DisplayName": "6824 Printer",
        "PrintHeadWidth": 960,
        "Initialize": "[0x1b,0x40,0x0d,0x00,0x00,0x00,0x00,0x00]",
        "NormalDotsHigh": 16, "NormalFont": "[0x1b,!,0x00]",
        "BoldIsFont": false, "BoldOff": "[0x1b,H]", "BoldOn": "[0x1b,G]",
        "CompressIsFont": false, "CompressDotsHigh": 12, "CompressOff": "[0x1b,0x35]", "CompressOn": "[0x1b,0x34]",
        "DoubleWideIsFont": false, "DoubleWideDotsHigh": 12, "DoubleWideOff": "[0x1b,0x0e]", "DoubleWideOn": "[0x1b,0x14]",
        "FormFeed": "[0x0c]",
        "ItalicIsFont": false, "ItalicOn": "[0x1b,0x34]", "ItalicOff": "[0x1b,0x35]",
        "UnderlineIsFont": false, "UnderlineOn": "[0x1b,0x2d,0x01]", "UnderlineOff": "[0x1b,0x2d,0x00]"
      },

      "PR2":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR2 Bt Printer",  "PrintHeadWidth": 384,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PR3":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR3 Bt Printer",  "PrintHeadWidth": 576,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB21":
      {
        "DisplayName":"PB21 Bt Printer", "PrintHeadWidth": 384, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 100, "PostGraphicsLineDelay": 1,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB31":
      {
        "DisplayName":"PB31 Bt Printer", "PrintHeadWidth": 576, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 100, "PostGraphicsLineDelay": 1,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB42":
      {
        "DisplayName": "PB42 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 1000,
        "BtConnectRetries": 4,"INCLUDE_02": "BARCODE_SETTINGS",
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB51":
      {
        "DisplayName":"PB51 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 500,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB22_Fingerprint":
      {
        "DisplayName":"PB22 Bt Label Printer", "PrintHeadWidth": 384, "LABEL_01": "2in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PB32_Fingerprint":
      {
        "DisplayName":"PB32 Bt Label Printer", "PrintHeadWidth": 576, "LABEL_01": "3in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PR_SETTINGS":
      {
        "PostGraphicsDelay": 100, "PostGraphicsLineDelay": 1, "PreCloseDelay": 1000
      },

      "BARCODE_SETTINGS":
      {
        "Code39NarrowWidth": 2, "Code39WideWidth": 4, "Code128NarrowWidth": 2
      }
    },

    "LABELS":
    {
      "2in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 30, 120:FT \"Swiss 721 Bold Condensed BT\",16:PT \"ItemName$$\":PP 120,75:BARSET \"CODE128\",3,1,4,150:PB \"ItemNo$$\":PP 280, 260:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 100, 20:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 200, 20:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 70,450:BARSET \"QRCODE\",1,1,8,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      },

      "3in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 40, 180:FT \"Swiss 721 Bold Condensed BT\",20:PT \"ItemName$$\":PP 150,180:BARSET \"CODE128\",3,1,4,240:PB \"ItemNo$$\":PP 400, 330:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 200, 50:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 300, 50:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 105,490:BARSET \"QRCODE\",1,1,13,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      }
    },
    "STATUS":
    {
      "ESCP_StatusQueries" :
      {
        "QUERIES" :
        [
          {
            "GetStatusCommand" : "[0x1b,{,S,T,?,}]",
            "StatusInitTimeout" : 5000,
            "StatusGapTimeout" : 500,
            "StatusEndResponse" : "[}]",
            "RESPONSES" :
            [
              { "PrinterMsg" : "[P,:,N]", "MsgNo" : 223 },
              { "PrinterMsg" : "[B,:,V]", "MsgNo" : 226 },
              { "PrinterMsg" : "[L,:,U]", "MsgNo" : 227 },
              { "PrinterMsg" : "[H,:,T]", "MsgNo" : 228 },
              { "PrinterMsg" : "[E,:,c]", "MsgNo" : 1001 },
              { "PrinterMsg" : "[E,:,d]", "MsgNo" : 1002 },
              { "PrinterMsg" : "[E,:,f]", "MsgNo" : 1003 },
              { "PrinterMsg" : "[E,:,g]", "MsgNo" : 1004 }
            ]
          }
        ]
      }
    }
  }
}
,
{
  "LINEPRINTERCONTROL":
  {
    "FormatVersion": "1.0.0.0",
    "Platform": "Windows_RT",
    "DEFAULTS":
    {
      "BoldIsFont": true, "BoldOff": "[0x1b,0x77,!]", "BoldOn": "[0x1b,0x77,0x6D]",
      "CompressDotsHigh": 12, "CompressIsFont": true, "CompressOff": "[0x1b,0x77,!]",
      "CompressOn": "[0x1b,0x77,0x42]", "DoubleHighDotsHigh": 32, "DoubleHighIsFont": false,
      "DoubleHighMask": 16, "DoubleHighOff": "[0x1b,!,0x10]", "DoubleHighOn": "[0x1b,!,0x10]",
      "DoubleWideDotsHigh": 16, "DoubleWideHighPrefix": "[0x1b,!]", "DoubleWideIsFont": false,
      "DoubleWideMask": 32, "DoubleWideOff": "[0x1b,!,0x20]", "DoubleWideOn": "[0x1b,!,0x20]",
      "Initialize": "[0x00,0x00,0x00,0x00,0x1b,0x40,0x00,0x00,0x00]",
      "NormalFont": "[0x1b,0x77,!,0x1b,!,0x00]", "NewLine": "[0x0d]", "NormalDotsHigh": 16,
      "FormFeed": "[0x0d,0x0d,0x0d,0x0d,0x0d]", "ValidateAttribOnFormFeed": true,
      "PrintHeadWidth": 384, "UseDoubleHWMask": true, "ValidateAttribOnFont": true,
      "ValidateAttribOnNewLn": true, "NullsBeforeClose": 0, "PreCloseDelay": 0, "EndOfGraphicsDelay": 0, 
      "PostGraphicsDelay": 0, "PostGraphicsLineDelay": 0, "PreGraphicsDelay": 0, "StartOfGraphicsDelay": 0,
      "BtConnectRetries" : 3, "BtconnectRetryDelay" : 100, "BtWriteDataReadyTimeout" : 10000, "BtWriteIntervalTimeout" : 10000, 
      "BtMaxSegWrite" : 1024, "BtMaxSegRead" : 1024, "BtLinger" : 10
    },

    "PRINTERS":
    {
      "P6824":
      {
        "DisplayName": "6824 Printer",
        "PrintHeadWidth": 960,
        "Initialize": "[0x1b,0x40,0x0d,0x00,0x00,0x00,0x00,0x00]",
        "NormalDotsHigh": 16, "NormalFont": "[0x1b,!,0x00]",
        "BoldIsFont": false, "BoldOff": "[0x1b,H]", "BoldOn": "[0x1b,G]",
        "CompressIsFont": false, "CompressDotsHigh": 12, "CompressOff": "[0x1b,0x35]", "CompressOn": "[0x1b,0x34]",
        "DoubleWideIsFont": false, "DoubleWideDotsHigh": 12, "DoubleWideOff": "[0x1b,0x0e]", "DoubleWideOn": "[0x1b,0x14]",
        "FormFeed": "[0x0c]",
        "ItalicIsFont": false, "ItalicOn": "[0x1b,0x34]", "ItalicOff": "[0x1b,0x35]",
        "UnderlineIsFont": false, "UnderlineOn": "[0x1b,0x2d,0x01]", "UnderlineOff": "[0x1b,0x2d,0x00]"
      },

      "PR2":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR2 Bt Printer",  "PrintHeadWidth": 384,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PR3":
      {
        "INCLUDE_01": "PR_SETTINGS",  "DisplayName":"PR3 Bt Printer",  "PrintHeadWidth": 576,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB21":
      {
        "DisplayName":"PB21 Bt Printer", "PrintHeadWidth": 384, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB31":
      {
        "DisplayName":"PB31 Bt Printer", "PrintHeadWidth": 576, "INCLUDE_02": "BARCODE_SETTINGS",
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10,
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB42":
      {
        "DisplayName":"PB42 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 4500,
        "NullsBeforeClose": 4000, "StartOfGraphicsDelay": 600, "INCLUDE_02": "BARCODE_SETTINGS",
        "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB51":
      {
        "DisplayName":"PB51 Bt Printer", "PrintHeadWidth": 832, "PreCloseDelay": 500,
        "INCLUDE_02": "BARCODE_SETTINGS", "STATUS_QUERIES" : "ESCP_StatusQueries"
      },

      "PB22_Fingerprint":
      {
        "DisplayName":"PB22 Bt Label Printer", "PrintHeadWidth": 384, "LABEL_01": "2in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PB32_Fingerprint":
      {
        "DisplayName":"PB32 Bt Label Printer", "PrintHeadWidth": 576, "LABEL_01": "3in_FingerprintLabels",
        "FormFeed": "[0x46,0x4F,0x52,0x4D,0x46,0x45,0x45,0x44]",
        "Initialize": [], "NormalFont": [], "NullsBeforeClose": 0, "PreCloseDelay": 1000
      },

      "PR_SETTINGS":
      {
        "PostGraphicsDelay": 1000, "PostGraphicsLineDelay": 10, "PreCloseDelay": 1000, "NullsBeforeClose": 8000
      },

      "BARCODE_SETTINGS":
      {
        "Code39NarrowWidth": 2, "Code39WideWidth": 4, "Code128NarrowWidth": 2
      }
    },

    "LABELS":
    {
      "2in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 30, 120:FT \"Swiss 721 Bold Condensed BT\",16:PT \"ItemName$$\":PP 120,75:BARSET \"CODE128\",3,1,4,150:PB \"ItemNo$$\":PP 280, 260:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 100, 20:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 200, 20:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 70,450:BARSET \"QRCODE\",1,1,8,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      },

      "3in_FingerprintLabels":
      {
        "ItemLabel":
        {
          "LabelDataStream": "DIR 4:AN 7:PP 40, 180:FT \"Swiss 721 Bold Condensed BT\",20:PT \"ItemName$$\":PP 150,180:BARSET \"CODE128\",3,1,4,240:PB \"ItemNo$$\":PP 400, 330:FT \"Letter Gothic 12 Pitch BT\",14:PT \"ItemNo$$\":PF\r\n",
          "VarPostfix": "$$"
        },

        "URL_QRLabel":
        {
          "StoreFormat": "INPUT ON\r\nLAYOUT INPUT \"URLQRLABEL.LAY\"\r\nDIR 4:AN 7:PP 200, 50:FT \"Swiss 721 Bold Condensed BT\",16:PT VAR1$:PP 300, 50:FT \"Letter Gothic 12 Pitch BT\",14:PT VAR2$:PP 105,490:BARSET \"QRCODE\",1,1,13,2,2:PB VAR3$\r\nLAYOUT END\r\nINPUT OFF\r\n",
          "InvokeFormat": "INPUT OFF\r\nFORMAT INPUT \"#\",\"@\",\"|\"\r\nINPUT ON\r\nLAYOUT RUN \"URLQRLABEL.LAY\"\r\n#qTextLine1$|qTextLine2$|qURL$|@\r\nPF\r\nINPUT OFF\r\n",
          "VarPrefix": "q",
          "VarPostfix": "$"
        }
      }
    },
    "STATUS":
    {
      "ESCP_StatusQueries" :
      {
        "QUERIES" :
        [
          {
            "GetStatusCommand" : "[0x1b,{,S,T,?,}]",
            "StatusInitTimeout" : 5000,
            "StatusGapTimeout" : 500,
            "StatusEndResponse" : "[}]",
            "RESPONSES" :
            [
              { "PrinterMsg" : "[P,:,N]", "MsgNo" : 223 },
              { "PrinterMsg" : "[B,:,V]", "MsgNo" : 226 },
              { "PrinterMsg" : "[L,:,U]", "MsgNo" : 227 },
              { "PrinterMsg" : "[H,:,T]", "MsgNo" : 228 },
              { "PrinterMsg" : "[E,:,c]", "MsgNo" : 1001 },
              { "PrinterMsg" : "[E,:,d]", "MsgNo" : 1002 },
              { "PrinterMsg" : "[E,:,f]", "MsgNo" : 1003 },
              { "PrinterMsg" : "[E,:,g]", "MsgNo" : 1004 }
            ]
          }
        ]
      }
    }
  }
}
];
