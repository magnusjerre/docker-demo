package com.example.bulletin.dto;

public class BulletinBoardMessageGetDto {
    private int id;
    private int posterId;
    private String message;

    public BulletinBoardMessageGetDto() {
    }

    public BulletinBoardMessageGetDto(int id, int posterId, String message) {
        this.id = id;
        this.posterId = posterId;
        this.message = message;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

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
