<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
    
<%@ page import="java.util.List" %>
<%@ page import="edu.usu.cloud.wr.UserSession"%>
<%@ page import="edu.usu.cloud.wr.ImageHandler"%>
<%@ page import="edu.usu.cloud.wr.model.Image"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Word Recognition</title>
</head>
<body>

<%  UserSession userSession = UserSession.getUserSession();
	ImageHandler imageHandler = userSession.getImageHandler();
%> 
	<h2>Nutrition Fact Images</h2>
	<table>
		<% List<Image> images = imageHandler.getImages(); 
		for(Image img: images)
		{
		%>
		<tr>
			<td style="width: 200px; height: 50px">
				<img alt="<%= img.getName() %>" src="<%= img.getUri() %>"/>
			</td>	
			<td style="width: 150px">
				<form method="get" action="/aws-web-word-recognition/rw">
					<input type="hidden" name="index" value="<%=images.indexOf(img) %>" />
					<input type="submit" value="Recognize Words"/>
				</form>
			</td>
			<td>
				<p><%=img.getRecognizedWords() %></p>
			</td>	
		</tr>
		<%
		}
		%>
	</table>
</body>
</html>