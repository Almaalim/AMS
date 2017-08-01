<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" encoding="utf-8"/>
 
  <xsl:template match="/Menus">
    <MenuItems>
      <xsl:call-template name="MenuListing" />
    </MenuItems>
  </xsl:template>
  
  
  <xsl:template name="MenuListing">
    <xsl:apply-templates select="Menu" />
  </xsl:template>
  
  <xsl:template match="Menu">
    <MenuItem>
     
      <xsl:attribute name="Text">
        <xsl:value-of select="MnuText"/>
      </xsl:attribute>

      <xsl:attribute name="MenuImageURL">
        <xsl:value-of select="MnuImageURL"/>
      </xsl:attribute>
      
      <xsl:attribute name="ToolTip">
        <xsl:value-of select="MnuTextEn"/>
      </xsl:attribute>
   
      
      <xsl:attribute name="NavigateUrl">  
        <xsl:value-of select="MnuURL"/>
      </xsl:attribute>

      <xsl:attribute name="Value">
        <xsl:value-of select="MnuPermissionID"/>
      </xsl:attribute>
      
        
      <xsl:if test="count(Menu) > 0">
        <xsl:call-template name="MenuListing" />
      </xsl:if>
      
    </MenuItem>
  </xsl:template>
</xsl:stylesheet>
