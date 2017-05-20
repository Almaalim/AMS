<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" encoding="utf-8"/>

  <xsl:template match="/Departments">
    <MenuItems>
      <xsl:call-template name="MenuListing" />
    </MenuItems>
  </xsl:template>


  <xsl:template name="MenuListing">
    <xsl:apply-templates select="Department" />
  </xsl:template>

  <xsl:template match="Department">
    <MenuItem>

      <xsl:attribute name="Text">
        <xsl:value-of select="DepNameEn"/>
      </xsl:attribute>

      <xsl:attribute name="Value">
        <xsl:value-of select="DepID"/>
      </xsl:attribute>

      <xsl:attribute name="Check">
        <xsl:value-of select="DepCheck"/>
      </xsl:attribute>

      <xsl:if test="count(Department) > 0">
        <xsl:call-template name="MenuListing" />
      </xsl:if>
    </MenuItem>
  </xsl:template>
</xsl:stylesheet>
