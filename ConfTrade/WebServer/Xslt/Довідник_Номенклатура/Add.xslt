﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">

    <h1>Довідник Номенклатура</h1>

    <p>
      <xsl:value-of select="/root/info"/>
    </p>

  </xsl:template>

</xsl:stylesheet>
