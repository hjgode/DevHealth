<?xml version="1.0" encoding="UTF-8" ?>

<!-- New document created with EditiX at Sun Jan 08 17:40:38 CET 2012 -->

<xsl:stylesheet version="2.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:fn="http://www.w3.org/2005/xpath-functions"
	xmlns:xdt="http://www.w3.org/2005/xpath-datatypes"
	xmlns:err="http://www.w3.org/2005/xqt-errors"
	exclude-result-prefixes="xs xdt err fn">

	<xsl:output method="text" indent="yes"/>
	
	
<xsl:template match="/">
	
<xsl:text>&lt;?xml version="1.0" encoding="UTF-8" ?&gt;
&lt;Subsystem Name="Device Monitor"&gt;
&lt;Group Name="ITCHealth"&gt;
</xsl:text>
	
<xsl:apply-templates />
<xsl:text>&lt;/Group&gt;
&lt;/Subsystem&gt;</xsl:text>
</xsl:template>
	
	<xsl:template match="element(Item)">
	<xsl:text>&lt;Field Name="</xsl:text>
	  <xsl:for-each select="ancestor::*">
	    
	    <xsl:value-of select="@Name" />

	    <xsl:text>\</xsl:text>
	  </xsl:for-each>
	  <xsl:value-of select="@Name" />
	  <xsl:text>"</xsl:text>
	  <xsl:text> Type="</xsl:text>
	  <xsl:value-of select="@Type" />
	  <xsl:text>"&gt;0&lt;</xsl:text>
	  <xsl:text>/Field&gt;</xsl:text>
	</xsl:template>

</xsl:stylesheet>
