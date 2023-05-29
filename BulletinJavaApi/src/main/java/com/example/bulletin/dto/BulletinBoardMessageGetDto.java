package com.example.bulletin.dto;

public class BulletinBoardMessageGetDto {
    private long id;
    private long posterId;
    private String message;

    public BulletinBoardMessageGetDto() {
    }

    public BulletinBoardMessageGetDto(long id, long posterId, String message) {
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

    public long getPosterId() {
        return posterId;
    }

    public void setPosterId(long posterId) {
        this.posterId = posterId;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
