<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet exclude-result-prefixes="msxsl js" version="1.0" xmlns="http://www.w3.org/1999/xhtml"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#" xmlns:js="urn:custom-javascript"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">

    <div xmlns="http://www.w3.org/1999/xhtml" id="container">
      <style>
        .patient span {
        font-weight: bold;
        text-align: left;
        margin-left: 10px;
		padding: 0px;
        }


        .department {
            text-align: center;
            }

        <!-- #department-name { -->
        <!-- display: inline-block; -->
        <!-- text-align: center; -->
		<!-- padding: 0px; -->
        <!-- } -->

        #time {
        text-align: center;
        display: block;
		padding: 0px;
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
        <div style ="width: 50%;float: left; text-align:certer; padding: 0px;">
          <div class="patient">
            <span id="patient-name" style="font-weight: bolder;margin-left: 20px;">
              <xsl:value-of select="//Patient//Name"/>
            </span>
			<span id="patient-name" style="font-weight: bolder;float:right; margin-right: 20px;">
              <xsl:value-of select="//Patient//NamSinh"/>
            </span>
          </div>
          <div class="department">
            <span id="department-name" style="text-align: center;">
              <xsl:value-of select="//Department/Name"/>
            </span>
          </div>
          <span id="time">
            <xsl:value-of select="//Time"/>
          </span>
          <div class="img" style ="text-align:center;width: 100%;padding: 0px;">
            <img>
              <xsl:attribute name="src">
                <xsl:value-of select="concat('data:image/gif;base64,',//QrCode/Img)"/>
              </xsl:attribute>
            </img>
            <span id="barcode-content" style="font-size:2.8em;font-weight: bolder;">
              <xsl:value-of select="//QrCode/Content"/>
            </span>
          </div>
		  <div class="sophieu" style="text-align: right;padding: 0px;">
            <span id="sophieu" style="font-size:1.65em;margin-right: 30px;margin-top: -10px;padding: 0px;">
              <xsl:value-of select="//SoPhieu//Name"/>
            </span>
          </div>
        </div>
      </xsl:for-each>
    </div>
  </xsl:template>
</xsl:stylesheet>