<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet exclude-result-prefixes="msxsl js" version="1.0" xmlns="http://www.w3.org/1999/xhtml"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#" xmlns:js="urn:custom-javascript"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">

    <div xmlns="http://www.w3.org/1999/xhtml" id="container" style="margin-top:0px">
      <style>
        .patient span {
        font-weight: bold;
        text-align: left;
        margin-left: 10px;
        }


        <!--.department {
            text-align: left;
            }-->

        #department-name {
        display: inline-block;
        text-align: left;
        margin-left: 10px;
        }

        #time {
        text-align: left;
        display: block;
        margin-left: 10px;
        }


        #barcode-content {
        text-align: center;
        display: block;
        font-weight: bolder;
        }

        #footer {
        display: block;
        float: right;
        }

        span {
        font-size: 2.2em;
        font-family: 'open sans', Helvetica Neue, Helvetica, Arial, sans-serif;
        }

        <!--img {
        width: 100px;
        height: 200px
        object-fit: contain;
        text-align:center;
        }-->
      </style>
      <xsl:for-each select="//LisSample/cd">
        <div style ="width: 50%;float: left; text-align:certer; ">
          <div class="patient">
            <span id="patient-name" style="font-weight: bolder;margin-left: 50px;">
              <xsl:value-of select="//Patient//Name"/>
            </span>
          </div>
          <div class="department">
            <span id="department-name" style="margin-left: 50px;">
              <xsl:value-of select="//Department/Name"/>
            </span>
          </div>
          <span id="time" style="margin-left: 50px;">
            <xsl:value-of select="//Time"/>
          </span>
          <div class="img" style ="text-align:center;width: 100%; margin-top:50px;">
            <img>
              <xsl:attribute name="src">
                <xsl:value-of select="concat('data:image/gif;base64,',//QrCode/Img)"/>
              </xsl:attribute>
            </img>
            <span id="barcode-content" style="font-size:2.8em;font-weight: bolder;">
              <xsl:value-of select="//QrCode/Content"/>
            </span>
          </div>
        </div>
      </xsl:for-each>
    </div>
  </xsl:template>
</xsl:stylesheet>