package com.example.bulletin.dto;

public class BulletinBoardMessagePostDto {
    private int posterId;
    private String message;

    public int getPosterId() {
        return posterId;
    }

    public void setPosterId(int posterId) {
        this.posterId = posterId;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
