package com.example.bulletin.dto;

public class BulletinBoardMessageGetDto {
    private long id;
    private String posterId;
    private String message;

    public BulletinBoardMessageGetDto() {
    }

    public BulletinBoardMessageGetDto(long id, String posterId, String message) {
        this.id = id;
        this.posterId = posterId;
        this.message = message;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

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
