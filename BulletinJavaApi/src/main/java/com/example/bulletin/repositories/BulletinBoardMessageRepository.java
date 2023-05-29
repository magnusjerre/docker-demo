package com.example.bulletin.repositories;

import com.example.bulletin.models.BulletinBoardMessage;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface BulletinBoardMessageRepository extends JpaRepository<BulletinBoardMessage, Long> {
}
