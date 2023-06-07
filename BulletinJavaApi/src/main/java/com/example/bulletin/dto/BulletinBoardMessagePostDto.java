package com.example.bulletin.dto;

public class BulletinBoardMessagePostDto {
    private String posterId;
    private String message;

    public String getPosterId() {
        return posterId;
    }

    public void setPosterId(String posterId) {
        this.posterId = posterId;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
