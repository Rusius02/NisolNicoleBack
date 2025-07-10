/*Remplissage de la base de donnée*/

-- ========== dbo_users ==========
INSERT INTO dbo.users (last_name, first_name, sexe, birthdate, mail, pseudo, password, role) VALUES
('Lievens', 'Justin', 'M', '2001-03-16', 'Justin123@gmail.com', 'TheGamers', '$2a$10$eXbhMVll8lQ7aYhEqNFiaeCYZrg9YKYq/pPCcbeE4T/qPWgJhlnn.', 'admin'),
('Vandeputte', 'Jeremy', 'M', '2000-08-19', 'Jeremy456@gmail.com', 'JeremyV', '$2a$10$1E6TIAmY1hPiBEkSQSbfr.OReEHJSLJh0f6Or5Dn8FrfznPemE912', 'user'),
('Stievenart', 'Adrien', 'M', '1997-10-02', 'Adrien123@gmail.com', 'Rusius', '$2a$10$A6Gap9k1CufpNlHbnf3//e6X9pgU63JfkEf5uKfBrImGkD66LLx5y', 'user'),
('Trufelli', 'Aleandro', 'M', '1997-10-02', 'Aleandro123@gmail.com', 'Aleandro', '$2a$10$S8fCCsauufcU7AcNJrFfUOSzHNPtBLVQ6.G.vBnYTWzF/pHa8jTEO', 'user');


-- ========== dbo_writing_event ==========
INSERT INTO dbo_writing_event (name, description, theme, start_date, end_date) VALUES
('Concours nouvelle romantique original N°1', 'Ecrivez une nouvelle abordant le thème de l''amour, une belle histoire d''amour qui ferait rêver les plus solitaires d''entre nous ou une histoire d''amour passionnelle mais destructrice. A vos plumes!', 'Romantisme', '2024-06-07 00:00:00', '2024-09-07 00:00:00'),
('Concours nouvelle romantique 1', 'Ecrivez une nouvelle abordant le thème de l''amour, une belle histoire d''amour qui ferait rêver les plus solitaires d''entre nous ou une histoire d''amour passionnelle mais destructrice. A vos plumes!', 'Romantisme', '2024-06-07 15:54:33', '2024-09-07 15:54:33'),
('Concours nouvelle romantique 2', 'Ecrivez une nouvelle abordant le thème de l''amour, une belle histoire d''amour qui ferait rêver les plus solitaires d''entre nous ou une histoire d''amour passionnelle mais destructrice. A vos plumes!', 'Romantisme', '2024-06-07 15:54:33', '2024-09-07 15:54:33'),
('Concours nouvelle romantique 3', 'Ecrivez une nouvelle abordant le thème de l''amour, une belle histoire d''amour qui ferait rêver les plus solitaires d''entre nous ou une histoire d''amour passionnelle mais destructrice. A vos plumes!', 'Romantisme', '2024-06-07 15:54:33', '2024-09-07 15:54:33');


-- ========== dbo_book ==========
INSERT INTO dbo.book (name, description, price, isbn, CoverImagePath, StripeProductId) VALUES
('Comme un vieux télégramme', 'Laura et Tom, une histoire d''amour d''adolescents, celui qui reste sage, on s''effleure du bout des lèvres, on se donne la main. Un jour la vie les éloigne. Ils continuent leur chemin, jusqu''à ce que Laura envoie un sms aux allures de vieux télégramme à Tom pour l''inviter à son mariage dans une vieille chapelle provençale.', 10.00, '978-2-8083-0599-0', '/images/covers/c4d336cd-2659-4a31-8cd0-c0b4d79a5864.jpeg', 'price_1PVU6hLttUxb3ZIRxJdy7nL6'),
('Alex', 'Découvrir Alex Ses différences, ses renaissances, son absolu... Et si Alex n''était pas celui que l''on croit...', 7.00, '978-2-8083-0729-1', '/images/covers/bbef9077-698e-44e2-9408-3bf5f7f1d563.jpeg', 'price_1PT5BHLttUxb3ZIRtkpZ1zuF'),
('La balançoire', 'Manon a quarante ans, elle se regarde dans le miroir. Elle se dit qu''elle n''a pas d''attache, plus de famille...Elle va partir avec médecin sans frontière. Mais elle reçoit une enveloppe qui va bouleverser sa vie.', 7.00, '978-2-8083-0752-9', '/images/covers/051d20c6-d53f-4cec-a347-2eea5b435d69.jpeg', 'price_1PVU4mLttUxb3ZIRRVOWVv6Q'),
('L''anagramme', 'Marie quitte un jour son village pour aller travailler à Bruxelles, elle n''a que treize ans. Elle y rencontre l''amour, mais cet amour est impossible. Le destin cruel va la marier au veuf de sa soeur pour élever les enfants. Ce qu''elle fera avec courage, avec tout l''amour qu''elle porte en elle.', 12.00, '978-2-8083-0747-5', '/images/covers/ae4ab5f4-a992-413f-8fa3-9bd20f3a81f1.jpeg', 'price_1PVU2tLttUxb3ZIR1qWeMO1F'),
('Un pommier sur la rive', 'Normandie dans les années 1950, Julia revient au village pour enseigner. Elle retrouve sa mère, qui n''a pas changé toujours en proie à la mélancolie, à la dépression. En même temps, sur les bords du Rhin, Emma coule des jours paisibles dans sa petite librairie. Des destins de femme liés par l''Histoire.', 12.00, '978-2-8083-1095-6', '/images/covers/6f674850-75d8-4021-82be-a3bcb1775b7a.jpeg', 'price_1PVTwdLttUxb3ZIR9ctLtFjt'),
('Kaléidoscope', 'L''autrice belge Claire Lussac retourne dans le village de son enfance. Elle a rendez-vous avec le notaire pour la vente de sa maison. En se promenant, elle revoit sa vie comme dans un kaléidoscope, images floues du passé.', 7.00, '978-2-8083-1107-6', '/images/covers/964fa9ac-dd50-4a47-b36a-fcd471103895.jpeg', 'price_1PVTtrLttUxb3ZIRpWj8i6WH'),
('L''eau de là', 'Un recueil de trois nouvelles qui ont pour décor un cours d''eau. Tantôt stagnante, près d''un marais, tantôt près de l''océan où un phare éteint ne guide plus les marins et une autre fois près d''un ruisseau qui peut surprendre par son cours tumultueux', 14.00, '978-2-8083-1465-7', '/images/covers/ca17f693-cc64-48fe-a7fc-3a4e1cf6decb.jpeg', 'price_1PVTr2LttUxb3ZIRXjwpenQq'),
('Sur le fil ténu de la vie', 'Sur le fil ténu de la vie, nous avançons tels des funambules. Dans le jardin des libellules, Lilly découvre que parfois on gomme une partie de sa vie, que l''on préfère oublier. La mémoire serait-elle sélective? Les cicatrices, parfois visibles, parfois cachées; si elles guérissent, laissent néanmoins des empreintes éternelles comme les floraisons des marronniers dans les cours d''école.', 7.00, '978-2-8083-2042-9', '/images/covers/06e929d0-b500-4955-ae7f-b21f647a3d2b.jpeg', 'price_1PVTpRLttUxb3ZIR0ZlPkORT'),
('Henriette noire gaillette', 'En vidant la maison de sa grand-mère partie au home, une dame découvre un cahier d''enfant. Il lui apprendra bien des secrets...', 5.00, '978-2-8083-2660-5', '/images/covers/308b0a31-0a4f-42e1-aa6e-daba21bdfb8b.jpeg', 'price_1PVTfKLttUxb3ZIRn6nfsl6y'),
('Au bord de l''estran', 'Ces six histoires vous emmènent au bord de la mer. Là où des choses un peu étranges vous surprendront.', 10.00, '978-2-8083-3065-7', '/images/covers/ac9e3565-a1f3-4eeb-95dd-79726ee8607d.jpeg', NULL);

-- ========== dbo_orders ==========
INSERT INTO dbo_orders (userId, amount, paymentStatus, createdAt, StripePaymentIntentId) VALUES
(3, 0.02, 'Pending', '2025-04-30 14:54:51', NULL),
(3, 0.02, 'Pending', '2025-04-30 15:20:55', NULL),
(3, 0.02, 'Pending', '2025-04-30 15:22:05', NULL),
(3, 0.02, 'requires_payment_method', '2025-04-30 15:22:42', 'pi_3RJcTPLttUxb3ZIR03iMmMzg'),
(3, 0.02, 'requires_payment_method', '2025-04-30 15:25:05', 'pi_3RJcVFLttUxb3ZIRdr6f3JFo'),
(3, 0.02, 'requires_payment_method', '2025-04-30 15:25:20', 'pi_3RJcVRLttUxb3ZIRldm6IGta'),
(3, 0.02, 'requires_payment_method', '2025-04-30 15:28:48', 'pi_3RJcVwLttUxb3ZIRhRaHgnB9'),
(3, 0.02, 'requires_payment_method', '2025-04-30 15:33:45', 'pi_3RJcWCLttUxb3ZIRSCxXObEh'),
(3, 0.02, 'requires_payment_method', '2025-04-30 15:38:16', 'pi_3RJcWRLttUxb3ZIR1pyTj4GC');
