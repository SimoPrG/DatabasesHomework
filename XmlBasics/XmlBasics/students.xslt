<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <body>
        <h2>Students</h2>
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Sex</th>
              <th>Birth date</th>
              <th>Phone</th>
              <th>E-mail</th>
              <th>Course</th>
              <th>Specialty</th>
              <th>Faculty Number</th>
              <th>Taken Exams</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="students/student">
              <tr>
                <td><xsl:value-of select="name"/></td>
                <td><xsl:value-of select="sex"/></td>
                <td><xsl:value-of select="birth_day"/></td>
                <td><xsl:value-of select="phone"/></td>
                <td><xsl:value-of select="email"/></td>
                <td><xsl:value-of select="course"/></td>
                <td><xsl:value-of select="specialty"/></td>
                <td><xsl:value-of select="faculty_number"/></td>
                <td>
                  <table>
                    <thead>
                      <tr>
                        <th>Exam</th>
                        <th>Tutor</th>
                        <th>Endorsements</th>
                        <th>Score</th>
                        <th>Date</th>
                      </tr>
                    </thead>
                    <tbody>
                      <xsl:for-each select="exams/exam">
                        <tr>
                          <td><xsl:value-of select="exam_name"/></td>
                          <td><xsl:value-of select="tutor/tutor_name"/></td>
                          <td><xsl:value-of select="tutor/endorsments"/></td>
                          <td><xsl:value-of select="score"/></td>
                          <td><xsl:value-of select="exam_date"/></td>
                        </tr>
                      </xsl:for-each>
                    </tbody>
                  </table>
                </td>
              </tr>
            </xsl:for-each>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
